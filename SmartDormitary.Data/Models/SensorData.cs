using System;
using System.ComponentModel.DataAnnotations;

namespace SmartDormitary.Data.Models
{
    public class SensorData
    {
        [Key] public int Id { get; set; }

        public DateTime? Timestamp { get; set; }

        public string Value { get; set; }
    }
}