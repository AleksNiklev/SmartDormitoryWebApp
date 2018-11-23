using SmartDormitary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitary.Models.SensorViewModels
{
    public class SensorTypeViewModel
    {
        public SensorTypeViewModel()
        {
        }

        public SensorTypeViewModel(SensorType sensorType)
        {
            this.Id = sensorType.Id;
            this.Tag = sensorType.Tag;
            this.MeasurementType = sensorType.MeasurementType;
            this.MinRefreshTime = sensorType.MinRefreshTime;
            this.MinAcceptableValue = sensorType.MinAcceptableValue;
            this.MaxAcceptableValue = sensorType.MaxAcceptableValue;
            this.Description = sensorType.Description;
        }

        public Guid Id { get; private set; }
        public string Tag { get; set; }
        public string MeasurementType { get; set; }
        public int MinRefreshTime { get; set; }
        public double MinAcceptableValue { get; set; }
        public double MaxAcceptableValue { get; set; }
        public string Description { get; set; }
    }
}
