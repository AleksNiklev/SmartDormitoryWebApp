using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Data.Models;

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
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}