using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartDormitary.Models.SensorViewModels
{
    public class SensorViewModel
    {
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

        public bool TickOff { get; set; }

        public bool IsPublic { get; set; }
    }
}
