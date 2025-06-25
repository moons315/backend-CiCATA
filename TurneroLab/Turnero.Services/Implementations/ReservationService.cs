using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;

namespace Turnero.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly TurneroDbContext _context;

        public ReservationService(TurneroDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync() =>
            await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Device)
                .ToListAsync();

        public async Task<Reservation> GetByIdAsync(int id) =>
            await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Device)
                .FirstOrDefaultAsync(r => r.Id == id)
            ?? throw new KeyNotFoundException($"Reserva {id} no encontrada");

        public async Task<Reservation> CreateAsync(int userId, int deviceId, DateTime startTime, DateTime endTime, string observations)
        {
            var reservation = new Reservation
            {
                UserId = userId,
                DeviceId = deviceId,
                StartTime = startTime,
                EndTime = endTime,
                Observations = observations,
                Status = ReservationStatus.Pendiente,
                CreatedAt = DateTime.UtcNow
            };
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task CancelAsync(int id)
        {
            var r = await GetByIdAsync(id);
            r.Status = ReservationStatus.Cancelada;
            await _context.SaveChangesAsync();
        }

        public async Task CancelByAdminAsync(int id)
        {
            // Mismo comportamiento que CancelAsync,
            // pero reservado solo para Administradores en el Controller
            var r = await GetByIdAsync(id);
            r.Status = ReservationStatus.Cancelada;
            await _context.SaveChangesAsync();
        }

        public async Task ExtendAsync(int id)
        {
            var r = await GetByIdAsync(id);
            r.Status = ReservationStatus.Extendida;
            r.EndTime = r.EndTime.AddMinutes(r.Device.DurationMinutes);
            await _context.SaveChangesAsync();
        }

        public async Task ReleaseAsync(int id)
        {
            var r = await GetByIdAsync(id);
            r.Status = ReservationStatus.Liberada;
            await _context.SaveChangesAsync();
        }

        public async Task CompleteAsync(int id)
        {
            var r = await GetByIdAsync(id);
            r.Status = ReservationStatus.Completada;
            await _context.SaveChangesAsync();
        }
    }
}
