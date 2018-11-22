using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartDormitary.Data.Models
{
    public class Sensor
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        public int RefreshTime { get; set; }

        public bool IsPublic { get; set; }

        public DateTime? Timestamp { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        
        [Required]
        public string Value { get; set; }

        public double MinAcceptableValue { get; set; }

        public double MaxAcceptableValue { get; set; }

        public bool TickOff { get; set; }

        public Guid SensorTypeId { get; set; }
        public SensorType SensorType { get; set; }

        public User User { get; set; }
    }
}