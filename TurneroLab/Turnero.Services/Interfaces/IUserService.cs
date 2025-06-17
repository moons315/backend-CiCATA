using System.Collections.Generic;
using System.Threading.Tasks;
using Turnero.Domain.Entities;

namespace Turnero.Services.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> CreateAsync(User user, string password);
        Task<bool> ValidateCredentialsAsync(string username, string password);
    }
}
