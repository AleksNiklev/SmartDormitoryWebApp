using System;
using System.Threading.Tasks;

namespace SmartDormitary.Services.Hubs.Service
{
    public interface IHubService
    {
        Task Notify(string userId, string name, string value, string measurementType);
        Task SensorUpdateData(string userId, Guid sensorId, string sensorTypeMeasurementType, string sensorDataValue, double sensorTypeMinAcceptableValue, double sensorTypeMaxAcceptableValue, double sensorMinAcceptableValue, double sensorMaxAcceptableValue);
    }
}