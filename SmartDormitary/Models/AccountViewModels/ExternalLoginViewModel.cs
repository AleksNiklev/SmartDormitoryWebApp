using System.ComponentModel.DataAnnotations;

namespace SmartDormitary.Models.AccountViewModels
{
    public class ExternalLoginViewModel
    {
        [Required] [EmailAddress] public string Email { get; set; }
    }
}