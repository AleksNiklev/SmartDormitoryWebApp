using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartDormitory.API.DormitaryAPI.Models;

namespace SmartDormitory.API.DormitaryAPI
{
    public interface ISensorsAPI
    {
        Task<IEnumerable<AllSensorsDTO>> GetAllSensorsAsync();
        Task<SensorDTO> GetSensorAsync(Guid sensorId);
    }
}