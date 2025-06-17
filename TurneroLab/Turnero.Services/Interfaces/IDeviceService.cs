using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface IDeviceService
    {
        // Devuelve todos los dispositivos
        Task<IEnumerable<Device>> GetAllAsync();

        // Devuelve un dispositivo por su ID
        Task<Device?> GetByIdAsync(int id);

        // Crea un nuevo dispositivo
        Task<Device> CreateAsync(Device device);

        // (Opcional) Lista los dispositivos de un laboratorio concreto
        Task<IEnumerable<Device>> GetByLabIdAsync(int labId);
    }
}
