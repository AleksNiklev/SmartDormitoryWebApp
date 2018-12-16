using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SmartDormitary.Services.Hubs.Service
{
    public class HubService : IHubService
    {
        private readonly IHubContext<NotifyHub> hubContext;

        public HubService(IHubContext<NotifyHub> hubContext)
        {
            this.hubContext = hubContext;
        }

        public async Task Notify(string userId, string name, string value, string measurementType)
        {
            await hubContext.Clients.User(userId).SendAsync("Notify", name, value, measurementType);
        }

        public async Task SensorUpdateData(string userId, Guid sensorId, string sensorTypeMeasurementType, string sensorDataValue, double sensorTypeMinAcceptableValue, double sensorTypeMaxAcceptableValue, double sensorMinAcceptableValue, double sensorMaxAcceptableValue)
        {
            await hubContext.Clients.User(userId).SendAsync("sensorUpdateData", sensorId, sensorTypeMeasurementType, sensorDataValue, sensorTypeMinAcceptableValue, sensorTypeMaxAcceptableValue, sensorMinAcceptableValue, sensorMaxAcceptableValue);
        }
    }
}