using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface IReservationService
    {
        // Obtener todas las reservas
        Task<IEnumerable<Reservation>> GetAllAsync();

        // Obtener una reserva por ID
        Task<Reservation?> GetByIdAsync(int id);

        // Crear una nueva reserva
        Task<Reservation> CreateAsync(Reservation reservation);

        // Extender la reserva existente (cambia EndTime)
        Task<Reservation> ExtendAsync(int reservationId, DateTime newEndTime, string? observations);

        // Liberar la reserva antes de tiempo (marca Finalizada)
        Task<Reservation> ReleaseAsync(int reservationId);

        // Marcar reserva como completada
        Task<Reservation> CompleteAsync(int reservationId);

        // Cancelar reserva
        Task CancelAsync(int reservationId);
    }
}
