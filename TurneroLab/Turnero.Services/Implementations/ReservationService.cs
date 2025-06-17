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

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Device)
                    .ThenInclude(d => d.Lab)
                .Include(r => r.Device)
                    .ThenInclude(d => d.Process)
                .Include(r => r.Extensions)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(r => r.User)
                .Include(r => r.Device)
                    .ThenInclude(d => d.Lab)
                .Include(r => r.Device)
                    .ThenInclude(d => d.Process)
                .Include(r => r.Extensions)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            reservation.Status = "Pendiente";
            reservation.CreatedAt = DateTime.UtcNow;
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> ExtendAsync(int reservationId, DateTime newEndTime, string? observations)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId)
                                 ?? throw new KeyNotFoundException("Reserva no encontrada");
            reservation.EndTime = newEndTime;
            reservation.Status = "Extendida";

            var extension = new ReservationExtension
            {
                ReservationId = reservationId,
                RequestedAt = DateTime.UtcNow,
                NewEndTime = newEndTime,
                Approved = true,
                Observations = observations ?? ""
            };

            _context.ReservationExtensions.Add(extension);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> ReleaseAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId)
                                 ?? throw new KeyNotFoundException("Reserva no encontrada");
            reservation.EndTime = DateTime.UtcNow;
            reservation.Status = "Finalizada";
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation> CompleteAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId)
                                 ?? throw new KeyNotFoundException("Reserva no encontrada");
            reservation.Status = "Finalizada";
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task CancelAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId)
                                 ?? throw new KeyNotFoundException("Reserva no encontrada");
            reservation.Status = "Cancelada";
            await _context.SaveChangesAsync();
        }
    }
}
