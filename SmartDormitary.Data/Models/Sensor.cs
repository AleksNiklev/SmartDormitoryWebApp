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

        [Required]
        [MaxLength(100)]
        public string Latitude { get; set; }
        [Required]
        [MaxLength(100)]
        public string Longitude { get; set; }

        [Required]
        public string Value { get; set; }

        public int TypeId { get; set; }
        public SensorType Type { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
    }
}