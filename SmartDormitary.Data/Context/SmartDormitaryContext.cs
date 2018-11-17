using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Data.Models;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.API.DormitaryAPI.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SmartDormitary.Data.Context
{
    public class SmartDormitaryContext : IdentityDbContext<User>
    {
        private readonly ISensorsAPI sensorApi; 

        public DbSet<Sensor> Sensors { get; set; }
        public DbSet<SensorType> SensorTypes { get; set; }

        public SmartDormitaryContext(ISensorsAPI sensorApi)
        {
            this.sensorApi = sensorApi;
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

        private async Task<IEnumerable<SensorType>> SeedSensorTypes()
        {
            var sensorsFromApi = await this.sensorApi.GetAllSensorsAsync();
            var sensorTypes = sensorsFromApi.Select(x => 
            {
                var numbers = GetNumbersFromString(x.Description);
                return new SensorType()
                {
                    Id = x.SensorId,
                    Tag = x.Tag,
                    Description = x.Description,
                    MinRefreshTime = x.RefreshTime,
                    MeasurementType = x.MeasureType,
                    MinAcceptableValue = numbers.Length == 0 ? 0 : numbers.Min(),
                    MaxAcceptableValue = numbers.Length == 0 ? 1 : numbers.Max()
                };
            });

            return sensorTypes;
        }

        private int[] GetNumbersFromString(string input)
        {
            List<int> result = new List<int>();
            string[] numbers = Regex.Split(input, @"\D+");
            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    result.Add(int.Parse(value));
                }
            }

            return result.ToArray();
        }
    }
}