using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartDormitary.Data.Context;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Hubs.Service;

namespace SmartDormitary.Services
{
    public class SensorsService : ISensorsService
    {
        private readonly SmartDormitaryContext dormitaryContext;
        private readonly IHubService hubService;

        public SensorsService(SmartDormitaryContext dormitaryContext, IHubService hubService)
        {
            this.hubService = hubService;
            this.dormitaryContext = dormitaryContext;
        }

        /// <summary>
        ///     Adds the given sensor in params as new sensor in the database.
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>ChangeTracking-EntityEntry</returns>
        public async Task<EntityEntry<Sensor>> RegisterSensorAsync(Sensor sensor)
        {
            var returnEntity = await dormitaryContext.Sensors.AddAsync(sensor);
            await dormitaryContext.SaveChangesAsync();
            return returnEntity;
        }

        /// <summary>
        ///     Updates the information in the database about the given sensor.
        /// </summary>
        /// <param name="sensor"></param>
        /// <returns>ChangeTracking-EntityEntry</returns>
        public async Task<EntityEntry<Sensor>> UpdateSensorAsync(Sensor sensor)
        {
            var returnEntity = dormitaryContext.Sensors.Update(sensor);
            await dormitaryContext.SaveChangesAsync();
            //await hubService.Notify(returnEntity.Entity.UserId, returnEntity.Entity.Name, returnEntity.Entity.Value, returnEntity.Entity.SensorType.MeasurementType);
            return returnEntity;
        }

        /// <summary>
        ///     Returns a sensors, matching the (Guid) sensorId.
        /// </summary>
        /// <param name="sensorId">(Guid) sensorId</param>
        /// <returns></returns>
        public async Task<Sensor> GetSensorByGuidAsync(Guid? sensorId)
        {
            return sensorId == null
                ? null
                : await dormitaryContext.Sensors.Where(s => s.Id == sensorId)
                    .Include(s => s.User)
                    .Include(s => s.SensorType)
                    .Include(s => s.SensorData)
                    .FirstOrDefaultAsync();
        }

        /// <summary>
        ///     Returns a collection of all sensors, registered by the given userId.
        /// </summary>
        /// <param name="userId">User's ID</param>
        /// <returns></returns>
        public async Task<List<Sensor>> GetUserSensorsAsync(string userId)
        {
            return await dormitaryContext.Sensors.Where(s => s.User.Id == userId)
                .Include(s => s.User)
                .Include(s => s.SensorData)
                .Include(s => s.SensorType)
                .ToListAsync();
        }

        /// <summary>
        ///     Returns a collection of all sensors that are public (isPublic).
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sensor>> GetAllPublicSensorsAsync()
        {
            return await dormitaryContext.Sensors.Where(s => s.IsPublic)
                .Include(s => s.User)
                .Include(s => s.SensorType)
                .Include(s => s.SensorData)
                .ToListAsync();
        }

        /// <summary>
        ///     Returns a collection of all sensors
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sensor>> GetAllSensorsAsync()
        {
            return await dormitaryContext.Sensors
                .Include(s => s.User)
                .Include(s => s.SensorType)
                .Include(s => s.SensorData)
                .ToListAsync();
        }

        /// <summary>
        ///     Returns a collection of all sensors
        /// </summary>
        /// <returns></returns>
        public List<Sensor> GetAllSensors()
        {
            return dormitaryContext.Sensors
                .Include(s => s.User)
                .Include(s => s.SensorType)
                .Include(s => s.SensorData)
                .ToList();
        }

        /// <summary>
        ///     Returns a sensors, matching the (Guid) sensorId.
        /// </summary>
        /// <param name="sensorId"></param>
        /// <returns></returns>
        public Sensor GetSensorByGuid(Guid sensorId)
        {
            return dormitaryContext.Sensors.Where(s => s.Id == sensorId)
                .Include(s => s.User)
                .Include(s => s.SensorData)
                .Include(s => s.SensorType).FirstOrDefault();
        }

        public async Task<EntityEntry<SensorData>> UpdateSensorDataAsync(Sensor sensor)
        {
            var sensorData = sensor.SensorData;
            var returnEntity = dormitaryContext.SensorData.Update(sensorData);
            await dormitaryContext.SaveChangesAsync();

            if (sensor.TickOff && (double.Parse(returnEntity.Entity.Value) > sensor.MaxAcceptableValue ||
                                   double.Parse(returnEntity.Entity.Value) < sensor.MinAcceptableValue))
                await hubService.Notify(sensor.UserId, sensor.Name, returnEntity.Entity.Value,
                    sensor.SensorType.MeasurementType);

            return returnEntity;
        }

        public async Task<EntityEntry<SensorData>> RegisterSensorDataAsync(SensorData sensorData)
        {
            var returnEntity = await dormitaryContext.SensorData.AddAsync(sensorData);
            await dormitaryContext.SaveChangesAsync();
            return returnEntity;
        }

        public async Task<int> GetSensorCountAsync()
        {
            return await dormitaryContext.Sensors.CountAsync();
        }

        public async Task<List<Sensor>> GetLastRegisteredSensorsAsync(int count = 10)
        {
            return await dormitaryContext.Sensors.OrderByDescending(t => t.CreatedOn).Take(count)
                .Include(s => s.User)
                .Include(s => s.SensorType)
                .Include(s => s.SensorData).ToListAsync();
        }

        public async Task DeleteSensorsAsync(Guid id)
        {
            var sensor = await dormitaryContext.Sensors.FindAsync(id);
            dormitaryContext.Sensors.Remove(sensor);
            dormitaryContext.SaveChanges();
        }

        public async Task<bool> SensorExists(Guid id)
        {
            return await dormitaryContext.Sensors.AnyAsync(e => e.Id == id);
        }
    }
}