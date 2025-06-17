using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface ILabService
    {
        Task<IEnumerable<Lab>> GetAllAsync();
        Task<Lab?> GetByIdAsync(int id);
        Task<Lab> CreateAsync(Lab lab);
    }
}
