using SmartDormitary.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitary.Models.SensorViewModels
{
    public class SensorViewModel
    {
        public SensorViewModel()
        {
        }

        public SensorViewModel(Sensor sensor)
        {
            this.Id = sensor.Id;
            this.Name = sensor.Name;
            this.Description = sensor.Description;
            this.Latitude = sensor.Latitude;
            this.Longitude = sensor.Longitude;
            this.IsPublic = sensor.IsPublic;
            this.MinAcceptableValue = sensor.MinAcceptableValue;
            this.MaxAcceptableValue = sensor.MaxAcceptableValue;
            this.Value = sensor.Value;
            this.PullingInterval = sensor.RefreshTime;
            this.TickOff = sensor.TickOff;
            this.Timestamp = sensor.Timestamp;
        }

        public SensorViewModel(SensorTypeViewModel sensorType)
        {
            this.Url = sensorType.Id.ToString();
            this.Type = sensorType;
        }

        public Guid Id { get; set; }

        [Required, MinLength(2), MaxLength(50)]
        public string Name { get; set; }

        [Required, MinLength(2), MaxLength(300)]
        public string Description { get; set; }

        [MinLength(2), MaxLength(50)]
        public string Url { get; set; }

        [Range(0, 2880)]
        public int PullingInterval { get; set; }

        [Range(-90, 90)]
        public double Latitude { get; set; }

        [Range(-180, 180)]
        public double Longitude { get; set; }

        [Required]
        public string Value { get; set; }

        public double MinAcceptableValue { get; set; }

        public double MaxAcceptableValue { get; set; }

        public DateTime? Timestamp { get; set; }

        public bool TickOff { get; set; }

        public bool IsPublic { get; set; }

        public SensorTypeViewModel Type { get; set; }
    }
}
