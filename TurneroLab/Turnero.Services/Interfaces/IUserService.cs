using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(string username, string email, string password, int roleId);
        Task<User> AuthenticateAsync(string username, string password);
    }
}
