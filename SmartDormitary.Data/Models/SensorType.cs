﻿using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace SmartDormitary.Data.Models
{
    public class SensorType
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public string MeasurementType { get; set; }

        public int MinRefreshTime { get; set; }
        public int MinAcceptableValue { get; set; }
        public int MaxAcceptableValue { get; set; }
    }
}