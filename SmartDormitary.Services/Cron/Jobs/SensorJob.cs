using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace SmartDormitary.Services.Cron.Jobs
{
    public class SensorJob : IJob
    {
        private readonly ISensorsAPI api;
        private readonly IServiceProvider serviceProvider;
        private readonly Guid sensorId;

        public SensorJob(ISensorsAPI api, IServiceProvider serviceProvider, Guid sensorId)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
            this.sensorId = sensorId;
        }

        public async void Execute()
        {
            using (var dbContext = this.serviceProvider.CreateScope()
                .ServiceProvider
                .GetService<SmartDormitaryContext>())
            {
                var sensorService = new SensorsService(dbContext);
                var sensor = await sensorService.GetSensorByGuidAsync(sensorId);

                var sensorApi = await this.api.GetSensorAsync(sensor.SensorTypeId);
                sensor.Value = sensorApi.Value;
                await sensorService.UpdateSensorAsync(sensor);
                Debug.WriteLine(sensor.Id + " was updated: value: " + sensor.Value);
            }
        }
    }
}
