using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Services.Contracts
{
    public interface ISensorTypesService
    {
        Task<SensorType> GetSensorTypeByIdAsync(Guid id);
        Task<List<SensorType>> GetAllSensorTypesAsync();
        Task<List<SensorType>> SeedSensorTypesAsync();
    }
}