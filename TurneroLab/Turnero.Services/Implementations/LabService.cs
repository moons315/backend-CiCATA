using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;

namespace Turnero.Services.Implementations
{
    public class LabService : ILabService
    {
        private readonly TurneroDbContext _context;

        public LabService(TurneroDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Lab>> GetAllAsync()
        {
            return await _context.Labs.ToListAsync();
        }

        public async Task<Lab?> GetByIdAsync(int id)
        {
            return await _context.Labs.FindAsync(id);
        }

        public async Task<Lab> CreateAsync(Lab lab)
        {
            _context.Labs.Add(lab);
            await _context.SaveChangesAsync();
            return lab;
        }
    }
}
