using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Infrastructure.Email;
using Turnero.Domain.Entities;   

namespace Turnero.Infrastructure.Background
{
    /// <summary>
    /// Servicio en segundo plano que:
    /// 1) Envía recordatorios 30’ antes de cada turno pendiente.
    /// 2) Finaliza reservas vencidas.
    /// 3) Notifica al siguiente usuario en cola cuando un equipo se libera.
    /// </summary>
    public class ReservationBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _provider;
        private readonly ILogger<ReservationBackgroundService> _logger;

        public ReservationBackgroundService(
            IServiceProvider provider,
            ILogger<ReservationBackgroundService> logger)
        {
            _provider = provider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("ReservationBackgroundService iniciado.");
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using var scope = _provider.CreateScope();
                    var db = scope.ServiceProvider.GetRequiredService<TurneroDbContext>();
                    var email = scope.ServiceProvider.GetRequiredService<IEmailService>();
                    var now = DateTime.UtcNow;

                    // 1) Recordatorios 30’ antes
                    var soon = now.AddMinutes(30);
                    var reminders = await db.Reservations
                        .Include(r => r.User)
                        .Include(r => r.Device)
                        .Where(r =>
                            r.Status == ReservationStatus.Pendiente &&
                            r.StartTime > now &&
                            r.StartTime <= soon
                        )
                        .ToListAsync(stoppingToken);

                    foreach (var r in reminders)
                    {
                        var body = $"<p>Hola {r.User.Username},<br/>" +
                                   $"Tu turno para «{r.Device.Name}» empieza a las {r.StartTime:HH:mm}.</p>";
                        await email.SendEmailAsync(r.User.Email, "Recordatorio de turno", body);
                        _logger.LogInformation("Recordatorio enviado a {email}", r.User.Email);
                    }

                    // 2) Finalizar reservas vencidas (Activa o Extendida)
                    var expired = await db.Reservations
                        .Include(r => r.User)
                        .Include(r => r.Device)
                        .Where(r =>
                            (r.Status == ReservationStatus.Activa ||
                             r.Status == ReservationStatus.Extendida) &&
                            r.EndTime <= now
                        )
                        .ToListAsync(stoppingToken);

                    foreach (var r in expired)
                    {
                        r.Status = ReservationStatus.Completada;
                        await db.SaveChangesAsync(stoppingToken);
                        _logger.LogInformation("Reserva {id} marcada como Completada", r.Id);

                        // 3) Notificar al siguiente en cola
                        var next = await db.Reservations
                            .Include(n => n.User)
                            .Include(n => n.Device)
                            .Where(n =>
                                n.DeviceId == r.DeviceId &&
                                n.Status == ReservationStatus.Pendiente &&
                                n.StartTime > now
                            )
                            .OrderBy(n => n.StartTime)
                            .FirstOrDefaultAsync(stoppingToken);

                        if (next != null)
                        {
                            var body2 = $"<p>Hola {next.User.Username},<br/>" +
                                        $"Tu turno para «{next.Device.Name}» ahora está disponible.</p>";
                            await email.SendEmailAsync(next.User.Email, "Equipo disponible", body2);
                            _logger.LogInformation("Notificación de disponibilidad enviada a {email}", next.User.Email);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error en ReservationBackgroundService");
                }

                // Esperar 5 minutos antes de la siguiente iteración
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
