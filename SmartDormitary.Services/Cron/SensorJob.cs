using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Cron.Jobs;
using SmartDormitary.Services.Hubs;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.API.DormitaryAPI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace SmartDormitary.Services.Cron
{
    public class SensorJob : IJob, ISensorJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap dataMap = context.JobDetail.JobDataMap;

            var api = (ISensorsAPI)dataMap.Get("api");
            var hubService = (IHubService)dataMap.Get("hubService");
            var dbContext = (SmartDormitaryContext)dataMap.Get("dbContext");

            var sensorService = new SensorsService(dbContext, hubService);

            var sensors = await sensorService.GetAllSensorsAsync();
            foreach (var sensor in sensors)
            {
                var sensorApi = await api.GetSensorAsync(sensor.SensorTypeId);

                if ((sensorApi.Timestamp.Value - sensor.Timestamp.Value).TotalSeconds > sensor.RefreshTime)
                {
                    sensor.Value = sensorApi.Value;
                    sensor.Timestamp = sensorApi.Timestamp;
                    await sensorService.UpdateSensorAsync(sensor);
                }

            }
        }
    }
}
