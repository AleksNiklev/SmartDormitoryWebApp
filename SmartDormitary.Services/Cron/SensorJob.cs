using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Quartz;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Cron.Jobs;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.API.DormitaryAPI;
using SmartDormitory.API.DormitaryAPI.Models;

namespace SmartDormitary.Services.Cron
{
    public class SensorJob : IJob, ISensorJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var dataMap = context.JobDetail.JobDataMap;
            var sensorApiValues = new Dictionary<Guid, SensorDTO>();

            var api = dataMap.Get("api") as ISensorsAPI;
            var hubService = dataMap.Get("hubService") as IHubService;
            var dbContext = dataMap.Get("dbContext") as SmartDormitaryContext;

            var sensorService = new SensorsService(dbContext, hubService);

            var sensors = await sensorService.GetAllSensorsAsync();
            foreach (var sensor in sensors)
            {
                if (!sensorApiValues.ContainsKey(sensor.SensorTypeId))
                {
                    var sensorDTO = await api.GetSensorAsync(sensor.SensorTypeId);
                    sensorApiValues[sensor.SensorTypeId] = sensorDTO;
                    Debug.WriteLine("****** Sensor " + sensor.Name);
                }

                var sensorApi = sensorApiValues[sensor.SensorTypeId];
                if ((sensorApi.Timestamp.Value - sensor.SensorData.Timestamp.Value).TotalSeconds >
                    sensor.RefreshTime)
                    {
                    sensor.SensorData.Value = sensorApi.Value;
                    sensor.SensorData.Timestamp = sensorApi.Timestamp;
                    await sensorService.UpdateSensorDataAsync(sensor);
                    Debug.WriteLine("****** Sensor value is" + sensor.SensorData.Value);
                    Debug.WriteLine("****** Sensor time" + sensor.SensorData.Timestamp);
                }
            }
        }
    }
}