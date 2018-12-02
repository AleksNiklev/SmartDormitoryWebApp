using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Services.Contracts
{
    public interface IUsersService
    {
        Task<EntityEntry<User>> AddUserAsync(User user);
        Task<List<User>> GetAllUsersAsync();
        Task<List<User>> GetLastRegisteredUsersAsync(int count = 10);
        Task<int> GetUsersCountAsync();
        Task<User> GetUserByGuidAsync(Guid? id);
        Task<bool> UserExistsAsync(Guid id);
        Task<EntityEntry<User>> UpdateUserAsync(User user);
        Task DeleteUserSensorsAsync(Guid id);
    }
}