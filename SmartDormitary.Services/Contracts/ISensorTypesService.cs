using System.Collections.Generic;
using System.Threading.Tasks;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Services.Contracts
{
    public interface ISensorTypesService
    {
        Task<List<SensorType>> SeedSensorTypesAsync();
    }
}