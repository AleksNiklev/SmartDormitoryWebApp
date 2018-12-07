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
            this.Value = sensor.SensorData.Value;
            this.PullingInterval = sensor.RefreshTime;
            this.TickOff = sensor.TickOff;
            this.Timestamp = sensor.SensorData.Timestamp;
            this.Type = new SensorTypeViewModel(sensor.SensorType);
            this.User = sensor.User;
        }

        public SensorViewModel(SensorTypeViewModel sensorType)
        {
            this.Type = sensorType;
            this.PullingInterval = sensorType.MinRefreshTime;
        }

        public Guid Id { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Invalid sensor name!")]
        [MaxLength(50, ErrorMessage = "Invalid sensor name!")]
        public string Name { get; set; }

        [Required]
        [MinLength(2, ErrorMessage = "Description is too short!")]
        [MaxLength(300, ErrorMessage = "Description is too long!")]
        public string Description { get; set; }
        
        [Range(0, 2880, ErrorMessage = "Invalid pulling interval!")]
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

        public User User { get; set; }
    }
}
