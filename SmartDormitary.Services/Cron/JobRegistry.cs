using FluentScheduler;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Cron.Jobs;
using SmartDormitary.Services.Hubs;
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
        private readonly IHubContext<NotifyHub> hubContext;

        public JobRegistry(ISensorsAPI api, IServiceProvider serviceProvider, IHubContext<NotifyHub> hubContext, Guid sensorId, int refreshTime)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
            this.hubContext = hubContext;

            Schedule(() => new SensorJob(api, this.serviceProvider, hubContext, sensorId))
                .ToRunNow()
                .AndEvery(refreshTime)
                .Seconds();
        }

        public JobRegistry(ISensorsAPI api, IServiceProvider serviceProvider, IHubContext<NotifyHub> hubContext)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
            this.sensorsService = serviceProvider.CreateScope().ServiceProvider.GetService<ISensorsService>();
            var sensors = this.sensorsService.GetAllSensors();

            Schedule(() => new SensorTypesJob(api, this.serviceProvider)).ToRunEvery(10).Minutes();
            foreach (var sensor in sensors)
            {
                Schedule(() => new SensorJob(api, this.serviceProvider, hubContext, sensor.Id))
                    .ToRunNow()
                    .AndEvery(sensor.RefreshTime)
                    .Seconds();
            }
        }
    }
}
