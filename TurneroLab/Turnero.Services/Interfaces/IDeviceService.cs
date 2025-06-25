using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface IDeviceService
    {
        Task<IEnumerable<Device>> GetAllAsync();
        Task<Device> GetByIdAsync(int id);
        Task<IEnumerable<Device>> GetByLabIdAsync(int labId);
        Task<Device> CreateAsync(string name, int labId, int durationMinutes, string configJson);
    }
}
