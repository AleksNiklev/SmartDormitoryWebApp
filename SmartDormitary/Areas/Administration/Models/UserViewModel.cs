using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
            Id = user.Id;
            Username = user.UserName;
            Email = user.Email;
            EmailConfirmed = user.EmailConfirmed;
            TwoFactorEnabled = user.TwoFactorEnabled;
            CreatedOn = user.CreatedOn;
            SensorsList = user.Sensors;

            RealUser = user;
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

        public User RealUser { get; set; }
    }
}