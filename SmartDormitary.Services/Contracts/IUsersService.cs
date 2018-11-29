using System.Collections.Generic;
using System.Threading.Tasks;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Services.Contracts
{
    public interface IUsersService
    {
        Task<List<User>> GetAllUsers();
        Task<List<User>> GetLastRegisteredUsersAsync(int count = 10);
        Task<int> GetUsersCountAsync();
    }
}