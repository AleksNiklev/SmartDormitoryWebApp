using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<int> GetUsersCountAsync()
        {
            return await dormitaryContext.Users.CountAsync();
        }

        public async Task<List<User>> GetLastRegisteredUsersAsync(int count = 10)
        {
            return await dormitaryContext.Users.OrderByDescending(t => t.CreatedOn).Take(count).Include(s => s.Sensors).ToListAsync();
        }
    }
}