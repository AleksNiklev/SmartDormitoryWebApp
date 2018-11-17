using System;
using RestSharp.Deserializers;

namespace SmartDormitory.API.DormitaryAPI.Models
{
    public class SensorDTO
    {
        [DeserializeAs(Name = "timeStamp")] public DateTime? Timestamp { get; set; }

        [DeserializeAs(Name = "value")] public string Value { get; set; }

        [DeserializeAs(Name = "valueType")] public string ValueType { get; set; }
    }
}