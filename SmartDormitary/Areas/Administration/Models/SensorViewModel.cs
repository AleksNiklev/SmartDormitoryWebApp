using System;
using System.ComponentModel.DataAnnotations;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Areas.Administration.Models
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
            RefreshTime = sensor.RefreshTime;
            IsPublic = sensor.IsPublic;
            CreatedOn = sensor.CreatedOn;
            Timestamp = sensor.SensorData.Timestamp;
            Latitude = sensor.Latitude;
            Longitude = sensor.Longitude;
            Value = sensor.SensorData.Value;
            MinAcceptableValue = sensor.MinAcceptableValue;
            MaxAcceptableValue = sensor.MaxAcceptableValue;
            TickOff = sensor.TickOff;
            SensorTypeId = sensor.SensorTypeId;
            SensorType = sensor.SensorType;
            UserId = sensor.UserId;
            User = sensor.User;
        }

        [Required] public Guid Id { get; set; }

        [Required] [MaxLength(50)] public string Name { get; set; }

        [Required] [MaxLength(300)] public string Description { get; set; }

        [Required]
        [Range(0, 604800)]
        [Display(Name = "Refresh Interval")]
        public int RefreshTime { get; set; }

        public bool IsPublic { get; set; }

        [Required] public DateTime CreatedOn { get; set; }

        public DateTime? Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public string Value { get; set; }

        [Required]
        [Display(Name = "Minimum Acceptable Value")]
        public double MinAcceptableValue { get; set; }

        [Required]
        [Display(Name = "Maximum Acceptable Value")]
        public double MaxAcceptableValue { get; set; }

        [Display(Name = "Notify on Critical Value")]
        public bool TickOff { get; set; }

        [Required] public Guid SensorTypeId { get; set; }

        public SensorType SensorType { get; set; }

        [Required] public string UserId { get; set; }

        public User User { get; set; }
    }
}