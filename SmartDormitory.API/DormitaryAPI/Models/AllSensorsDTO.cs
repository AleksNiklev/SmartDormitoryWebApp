using System;
using RestSharp.Deserializers;

namespace SmartDormitary.DormitaryAPI.Models
{
    public class AllSensorsDTO
    {
        [DeserializeAs(Name = "sensorId")] public Guid SensorId { get; set; }

        [DeserializeAs(Name = "tag")] public string Tag { get; set; }

        [DeserializeAs(Name = "description")] public string Description { get; set; }

        [DeserializeAs(Name = "minPollingIntervalInSeconds")]
        public int RefreshTime { get; set; }

        [DeserializeAs(Name = "measureType")] public string MeasureType { get; set; }
    }
}