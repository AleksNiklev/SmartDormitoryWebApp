using System;
using System.ComponentModel.DataAnnotations;

namespace SmartDormitary.Models.SensorViewModels
{
    public class RegisterSensorViewModel
    {
        public RegisterSensorViewModel()
        {
        }

        public RegisterSensorViewModel(SensorTypeViewModel sensor)
        {
            Id = sensor.Id;
            MeasurementType = sensor.MeasurementType;
            DefaultPullingInterval = sensor.MinRefreshTime;
            DefaultMinAcceptableValue = sensor.MinAcceptableValue;
            DefaultMaxAcceptableValue = sensor.MaxAcceptableValue;
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

        public double MinAcceptableValue { get; set; }

        public double MaxAcceptableValue { get; set; }

        public int? DefaultPullingInterval { get; set; }

        public double? DefaultMinAcceptableValue { get; set; }

        public double? DefaultMaxAcceptableValue { get; set; }

        public string MeasurementType { get; set; }

        public bool TickOff { get; set; }

        public bool IsPublic { get; set; }
    }
}