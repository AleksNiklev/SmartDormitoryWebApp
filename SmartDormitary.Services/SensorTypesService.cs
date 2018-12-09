using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Extensions;
using SmartDormitory.API.DormitaryAPI;

namespace SmartDormitary.Services
{
    public class SensorTypesService : ISensorTypesService
    {
        private readonly SmartDormitaryContext dormitaryContext;
        private readonly ISensorsAPI sensorsApi;

        public SensorTypesService(SmartDormitaryContext dormitaryContext, ISensorsAPI sensorsApi)
        {
            this.dormitaryContext = dormitaryContext;
            this.sensorsApi = sensorsApi;
        }

        public async Task<SensorType> GetSensorTypeByIdAsync(Guid id)
        {
            return await dormitaryContext.SensorTypes.Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<SensorType>> GetAllSensorTypesAsync()
        {
            return await dormitaryContext.SensorTypes.ToListAsync();
        }

        public async Task<List<SensorType>> SeedSensorTypesAsync()
        {
            var sensorsFromApi = await sensorsApi.GetAllSensorsAsync();
            foreach (var sensorType in sensorsFromApi)
            {
                if (await dormitaryContext.SensorTypes.AnyAsync(s => s.Id == sensorType.SensorId)) continue;

                var numbers = Helpers.GetNumbersFromString(sensorType.Description);
                var tempSensor = new SensorType
                {
                    Id = sensorType.SensorId,
                    Tag = sensorType.Tag,
                    Description = sensorType.Description,
                    MinRefreshTime = sensorType.RefreshTime,
                    MeasurementType = sensorType.MeasureType,
                    MinAcceptableValue = numbers.Length == 0 ? 0 : numbers.Min(),
                    MaxAcceptableValue = numbers.Length == 0 ? 1 : numbers.Max()
                };
                dormitaryContext.SensorTypes.Add(tempSensor);
            }

            await dormitaryContext.SaveChangesAsync();
            return await dormitaryContext.SensorTypes.ToListAsync();
        }
    }
}