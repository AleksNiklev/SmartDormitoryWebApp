using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Cron.Jobs;
using SmartDormitory.API.DormitaryAPI;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartDormitary.Services.Cron
{
    public class JobRegistry : Registry
    {
        private readonly ISensorsAPI api;
        private readonly IServiceProvider serviceProvider;
        private readonly ISensorsService sensorsService;

        public JobRegistry(ISensorsAPI api, IServiceProvider serviceProvider, Guid sensorId, int refreshTime)
        {
            this.api = api;
            this.serviceProvider = serviceProvider; 

            Schedule(() => new SensorJob(api, this.serviceProvider, sensorId))
                .ToRunNow()
                .AndEvery(refreshTime)
                .Seconds();
        }

        public JobRegistry(ISensorsAPI api, IServiceProvider serviceProvider)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
            this.sensorsService = serviceProvider.CreateScope().ServiceProvider.GetService<ISensorsService>();
            var sensors = this.sensorsService.GetAllSensors();

            Schedule(() => new SensorTypesJob(api, this.serviceProvider)).ToRunEvery(10).Minutes();
            foreach (var sensor in sensors)
            {
                Schedule(() => new SensorJob(api, this.serviceProvider, sensor.Id))
                    .ToRunNow()
                    .AndEvery(sensor.RefreshTime)
                    .Seconds();
            }
        }
    }
}
