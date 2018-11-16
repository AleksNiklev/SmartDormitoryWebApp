using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using RestSharp;
using SmartDormitary.Services.API.Models;
using SmartDormitary.Services.API.Utils;

namespace SmartDormitary.Services.API
{
    public class SensorsAPI
    {
        private const string getAllSensorsUrl = "api/sensor/all";
        private const string getSensorIdUrl = "api/sensor/SensorId";
        private readonly IRestClient restClient;

        public SensorsAPI(IRestClient restClient)
        {
            this.restClient = restClient;
        }

        /// <summary>
        ///     Returns enumerable collection containing all the sensors in the API.
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AllSensorsDTO>> GetAllSensorsAsync()
        {
            restClient.BaseUrl = new Uri(Constants.baseUrl);
            restClient.AddDefaultHeader(Constants.authKey, Constants.authValue);

            var request = new RestRequest(Method.GET) {Resource = getAllSensorsUrl};

            var response = await restClient.ExecuteGetTaskAsync<IEnumerable<AllSensorsDTO>>(request);
            return response.Data;
        }

        /// <summary>
        ///     Returns adequate data for the given sensorId.
        /// </summary>
        /// <param name="sensorId">Note: AddParameter is using nameof(sensorId)</param>
        /// <returns></returns>
        public async Task<SensorDTO> GetSensorAsync(Guid sensorId)
        {
            restClient.BaseUrl = new Uri(Constants.baseUrl);
            restClient.AddDefaultHeader(Constants.authKey, Constants.authValue);

            var request = new RestRequest(Method.GET) {Resource = getSensorIdUrl};
            request.AddParameter(nameof(sensorId), sensorId);

            var response = await restClient.ExecuteGetTaskAsync<SensorDTO>(request);
            return response.Data;
        }
    }
}