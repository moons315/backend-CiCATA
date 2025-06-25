using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<Device>> GetAllAsync() =>
            await _context.Devices.ToListAsync();

        public async Task<Device> GetByIdAsync(int id) =>
            await _context.Devices.FindAsync(id)
                ?? throw new KeyNotFoundException($"Dispositivo {id} no encontrado");

        public async Task<IEnumerable<Device>> GetByLabIdAsync(int labId) =>
            await _context.Devices
                .Where(d => d.LabId == labId)
                .ToListAsync();

        public async Task<Device> CreateAsync(string name, int labId, int durationMinutes, string configJson)
        {
            var device = new Device
            {
                Name = name,
                LabId = labId,
                DurationMinutes = durationMinutes,
                ConfigJson = configJson
            };
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return device;
        }
    }
}
