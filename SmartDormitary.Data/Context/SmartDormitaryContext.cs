using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Data.Models;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.API.DormitaryAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SmartDormitary.Data.Context
{
    public class SmartDormitaryContext : IdentityDbContext<User>
    {
        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }

        public SmartDormitaryContext()
        {
        }

        public SmartDormitaryContext(DbContextOptions<SmartDormitaryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole {Name = "Administrator", NormalizedName = "ADMINISTRATOR"});

            base.OnModelCreating(builder);
        }
    }
}