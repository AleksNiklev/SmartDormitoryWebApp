using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Services.Contracts
{
    public interface ISensorsService
    {
        Task<List<Sensor>> GetAllPublicSensorsAsync();
        Task<List<Sensor>> GetSensorByGuidAsync(Guid sensorId);
        Task<List<Sensor>> GetUserSensorsAsync(string userId);
        Task<EntityEntry<Sensor>> RegisterSensorAsync(Sensor sensor);
        Task<EntityEntry<Sensor>> UpdateSensorAsync(Sensor sensor);
    }
}