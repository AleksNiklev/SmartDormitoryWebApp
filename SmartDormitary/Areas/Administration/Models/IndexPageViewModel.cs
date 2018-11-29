using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Areas.Administration.Models
{
    public class IndexPageViewModel
    {
        public IList<User> UsersList { get; set; }
        public IList<Sensor> SensorsList { get; set; }
        public int UsersCount { get; set; }
        public int SensorsCount { get; set; }
    }
}