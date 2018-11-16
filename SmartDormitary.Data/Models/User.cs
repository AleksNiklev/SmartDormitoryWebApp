using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace SmartDormitary.Data.Models
{
    public class User : IdentityUser
    {
        public IList<Sensor> Sensors { get; set; } = new List<Sensor>();
    }
}