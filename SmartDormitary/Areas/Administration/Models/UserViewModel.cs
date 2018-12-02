using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SmartDormitary.Data.Models;

namespace SmartDormitary.Areas.Administration.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            
        }

        public UserViewModel(User user)
        {
            this.Id = user.Id;
            this.Username = user.UserName;
            this.Email = user.Email;
            this.EmailConfirmed = user.EmailConfirmed;
            this.TwoFactorEnabled = user.TwoFactorEnabled;
            this.CreatedOn = user.CreatedOn;
            this.SensorsList = user.Sensors;
        }

        public string Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4)]
        public string Username { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime CreatedOn { get; set; }
        public IList<Sensor> SensorsList { get; set; }
    }
}