using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
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
            this.Id = sensor.Id;
            this.Name = sensor.Name;
            this.Description = sensor.Description;
            this.RefreshTime = sensor.RefreshTime;
            this.IsPublic = sensor.IsPublic;
            this.CreatedOn = sensor.CreatedOn;
            //this.Timestamp = sensor.Timestamp;
            this.Latitude = sensor.Latitude;
            this.Longitude = sensor.Longitude;
            //this.Value = sensor.Value;
            this.MinAcceptableValue = sensor.MinAcceptableValue;
            this.MaxAcceptableValue = sensor.MaxAcceptableValue;
            this.TickOff = sensor.TickOff;
            this.SensorTypeId = sensor.SensorTypeId;
            this.SensorType = sensor.SensorType;
            this.UserId = sensor.UserId;
            this.User = sensor.User;
        }

        [Required]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [Range(0, 604800)]
        [Display(Name = "Refresh Interval")]
        public int RefreshTime { get; set; }

        public bool IsPublic { get; set; }

        [Required]
        public DateTime CreatedOn { get; set; }
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

        [Required]
        public Guid SensorTypeId { get; set; }
        public SensorType SensorType { get; set; }

        [Required]
        public string UserId { get; set; }
        public User User { get; set; }
    }
}