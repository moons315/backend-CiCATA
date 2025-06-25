using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation> GetByIdAsync(int id);
        Task<Reservation> CreateAsync(int userId, int deviceId, DateTime startTime, DateTime endTime, string observations);
        Task CancelAsync(int id);
        Task CancelByAdminAsync(int id);
        Task ExtendAsync(int id);
        Task ReleaseAsync(int id);
        Task CompleteAsync(int id);
    }
}