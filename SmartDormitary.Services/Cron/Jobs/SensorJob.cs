using FluentScheduler;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Hubs;
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
        private readonly IHubContext<NotifyHub> hubContext;
        private readonly Guid sensorId;

        public SensorJob(ISensorsAPI api, IServiceProvider serviceProvider, IHubContext<NotifyHub> hubContext, Guid sensorId)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
            this.sensorId = sensorId;
            this.hubContext = hubContext;
        }

        public async void Execute()
        {
            try
            {
                using (var dbContext = this.serviceProvider.CreateScope()
                    .ServiceProvider
                    .GetService<SmartDormitaryContext>())
                {
                    var sensorService = new SensorsService(dbContext, this.hubContext);
                    var sensor = await sensorService.GetSensorByGuidAsync(sensorId);

                    var sensorApi = await this.api.GetSensorAsync(sensor.SensorTypeId);
                    sensor.Value = sensorApi.Value;
                    sensor.Timestamp = sensorApi.Timestamp;
                    await sensorService.UpdateSensorAsync(sensor);
                    Debug.WriteLine(sensor.Id + " was updated: value: " + sensor.Value);
                    Debug.WriteLine(sensor.Id + " was updated: TimeStamp: " + sensor.Timestamp);
                }

            }
            catch (Exception ex)
            {
                throw ex;
                Debug.WriteLine("Got Execption in job: " + ex.Message);
            }
        }
    }
}
