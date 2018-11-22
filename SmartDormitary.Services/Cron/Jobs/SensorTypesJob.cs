using FluentScheduler;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Contracts;
using SmartDormitory.API.DormitaryAPI;
using System;
using System.Diagnostics;

namespace SmartDormitary.Services.Cron.Jobs
{
    public class SensorTypesJob : IJob
    {
        private readonly ISensorsAPI api;
        private readonly IServiceProvider serviceProvider;

        public SensorTypesJob(ISensorsAPI api, IServiceProvider serviceProvider)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
        }

        public async void Execute()
        {
            using (var dbContext = serviceProvider.CreateScope()
                .ServiceProvider
                .GetService<SmartDormitaryContext>())
            {
                var sensorTypeService = new SensorTypesService(dbContext, this.api);
                var sensorTypes = await sensorTypeService.SeedSensorTypesAsync();
                foreach (var type in sensorTypes)
                {

                    Debug.WriteLine(type.Id + " ********* " + type.Tag);
                }
            }
        }
    }
}
