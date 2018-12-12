using System;
using System.ComponentModel.DataAnnotations;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Models.SensorViewModels
{
    public class SensorViewModel
    {
        public SensorViewModel()
        {
        }

        public SensorViewModel(Sensor sensor)
        {
            Id = sensor.Id;
            Name = sensor.Name;
            Description = sensor.Description;
            Latitude = sensor.Latitude;
            Longitude = sensor.Longitude;
            IsPublic = sensor.IsPublic;
            MinAcceptableValue = sensor.MinAcceptableValue;
            MaxAcceptableValue = sensor.MaxAcceptableValue;
            Value = sensor.SensorData.Value;
            PullingInterval = sensor.RefreshTime;
            TickOff = sensor.TickOff;
            Timestamp = sensor.SensorData.Timestamp;
            Type = new SensorTypeViewModel(sensor.SensorType);
            User = sensor.User;
        }

        public SensorViewModel(SensorTypeViewModel sensorType)
        {
            Type = sensorType;
            PullingInterval = sensorType.MinRefreshTime;
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

        [Range(-90, 90)] public double Latitude { get; set; }

        [Range(-180, 180)] public double Longitude { get; set; }

        [Required] public string Value { get; set; }

        public double MinAcceptableValue { get; set; }

        public double MaxAcceptableValue { get; set; }

        public DateTime? Timestamp { get; set; }

        public bool TickOff { get; set; }

        public bool IsPublic { get; set; }

        public SensorTypeViewModel Type { get; set; }

        public User User { get; set; }
    }
}