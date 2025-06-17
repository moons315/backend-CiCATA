using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;

namespace Turnero.Services.Implementations
{
    public class DeviceService : IDeviceService
    {
        private readonly TurneroDbContext _context;

        public DeviceService(TurneroDbContext context)
        {
            _context = context;
        }

        // Devuelve todos los dispositivos, incluyendo datos de Lab y Process
        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await _context.Devices
                .Include(d => d.Lab)
                .Include(d => d.Process)
                .ToListAsync();
        }

        // Devuelve un dispositivo por su ID
        public async Task<Device?> GetByIdAsync(int id)
        {
            return await _context.Devices
                .Include(d => d.Lab)
                .Include(d => d.Process)
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        // Crea un nuevo dispositivo
        public async Task<Device> CreateAsync(Device device)
        {
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }

        // Lista dispositivos filtrando por laboratorio
        public async Task<IEnumerable<Device>> GetByLabIdAsync(int labId)
        {
            return await _context.Devices
                .Where(d => d.LabId == labId)
                .Include(d => d.Process)
                .ToListAsync();
        }
    }
}
