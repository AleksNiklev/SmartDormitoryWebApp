using System;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Models.SensorViewModels
{
    public class SensorTypeViewModel
    {
        public SensorTypeViewModel()
        {
        }

        public SensorTypeViewModel(SensorType sensorType)
        {
            Id = sensorType.Id;
            Tag = sensorType.Tag;
            MeasurementType = sensorType.MeasurementType;
            MinRefreshTime = sensorType.MinRefreshTime;
            MinAcceptableValue = sensorType.MinAcceptableValue;
            MaxAcceptableValue = sensorType.MaxAcceptableValue;
            Description = sensorType.Description;
        }

        public Guid Id { get; }
        public string Tag { get; set; }
        public string MeasurementType { get; set; }
        public int MinRefreshTime { get; set; }
        public double MinAcceptableValue { get; set; }
        public double MaxAcceptableValue { get; set; }
        public string Description { get; set; }
    }
}