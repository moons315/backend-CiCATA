using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;
using Turnero.Services.DTOs;

namespace Turnero.Services.Implementations
{
    public class ReportService : IReportService
    {
        private readonly TurneroDbContext _context;

        public ReportService(TurneroDbContext context)
        {
            _context = context;
        }

        public async Task<CancellationsReportDto> GetCancellationsAsync()
        {
            // Total de reservas creadas
            var total = await _context.Reservations.CountAsync();

            // Cancelaciones hechas por usuarios sobre sus propias reservas.
            // (Aquí asumimos que en la entidad Reservation guardas quién canceló;
            // si no, en esta primera versión contaré TODAS las canceladas como “byUser”)
            var byUser = await _context.Reservations
                .CountAsync(r => r.Status == ReservationStatus.Cancelada);

            // Cancelaciones hechas por admin (a futuro, filtrar según campo “CancelledByAdmin”)
            var byAdmin = 0;

            var rate = total > 0
                ? byUser / (double)total * 100
                : 0;

            return new CancellationsReportDto(byUser, byAdmin, rate);
        }
    }
}
