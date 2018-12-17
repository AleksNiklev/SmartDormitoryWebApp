using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace SmartDormitary.Data.Models
{
    public class User : IdentityUser
    {
        public IList<Sensor> Sensors { get; set; } = new List<Sensor>();
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public bool AcceptedGDPR { get; set; }
    }
}