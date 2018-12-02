using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;

namespace SmartDormitary.Services
{
    public class UsersService : IUsersService
    {
        private readonly SmartDormitaryContext dormitaryContext;

        public UsersService(SmartDormitaryContext dormitaryContext)
        {
            this.dormitaryContext = dormitaryContext;
        }

        public async Task<EntityEntry<User>> AddUserAsync(User user)
        {
            var temp = await dormitaryContext.Users.AddAsync(user);
            await this.dormitaryContext.SaveChangesAsync();
            return temp;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await dormitaryContext.Users.Include(s => s.Sensors).ToListAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await dormitaryContext.Users.CountAsync();
        }

        public async Task<List<User>> GetLastRegisteredUsersAsync(int count = 10)
        {
            return await dormitaryContext.Users.OrderByDescending(t => t.CreatedOn).Take(count).Include(s => s.Sensors).ToListAsync();
        }

        public async Task<User> GetUserByGuidAsync(Guid? id)
        {
            return await dormitaryContext.Users.Include(u => u.Sensors).ThenInclude(st => st.SensorType).SingleOrDefaultAsync(u => u.Id == id.ToString());
        }

        public async Task<bool> UserExistsAsync(Guid id)
        {
            return await dormitaryContext.Users.AnyAsync(u => u.Id == id.ToString());
        }

        public async Task<EntityEntry<User>> UpdateUserAsync(User user)
        {
            var returnEntity = dormitaryContext.Users.Update(user);
            await dormitaryContext.SaveChangesAsync();
            return returnEntity;
        }

        public async Task DeleteUserSensorsAsync(Guid id)
        {
            var sensors = await dormitaryContext.Sensors.Where(s => s.UserId == id.ToString()).ToListAsync();
            foreach (var sensor in sensors)
            {
                dormitaryContext.Sensors.Remove(sensor);
            }

            await dormitaryContext.SaveChangesAsync();
        }
    }
}