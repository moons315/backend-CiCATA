// Turnero.Services/Implementations/UserService.cs
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Turnero.Data.Context;
using Turnero.Domain.Entities;
using Turnero.Services.Interfaces;
using Turnero.Services.Exceptions; // Donde definimos AppException

namespace Turnero.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly TurneroDbContext _context;

        public UserService(TurneroDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetByIdAsync(int id) =>
            await _context.Users.FindAsync(id);

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users.ToListAsync();

        public async Task<User> CreateAsync(string username, string email, string password, int roleId)
        {
            // Comprueba duplicados
            if (await _context.Users.AnyAsync(u => u.Email == email))
                throw new AppException("El correo ya está registrado");

            var user = new User
            {
                Username = username,
                Email = email,
                RoleId = roleId,
                CreatedAt = DateTime.UtcNow,
                PasswordHash = ComputeHash(password)
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> AuthenticateAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .SingleOrDefaultAsync(u => u.Username == username);

            if (user == null || user.PasswordHash != ComputeHash(password))
                throw new AppException("Usuario o contraseña incorrectos");

            return user;
        }

        private static string ComputeHash(string input)
        {
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(bytes);
        }
    }
}
