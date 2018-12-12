using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Data.Context
{
    public class SmartDormitaryContext : IdentityDbContext<User>
    {
        public SmartDormitaryContext()
        {
        }

        public SmartDormitaryContext(DbContextOptions<SmartDormitaryContext> options)
            : base(options)
        {
        }

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorData> SensorData { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // We don't need to seed the roles anymore.
            builder.Entity<IdentityRole>()
                .HasData(new IdentityRole {Name = "Administrator", NormalizedName = "ADMINISTRATOR"});
            //builder.Entity<IdentityRole>()
            //    .HasData(new IdentityRole { Name = "User", NormalizedName = "USER" });

            base.OnModelCreating(builder);
        }
    }
}