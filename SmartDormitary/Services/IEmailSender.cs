using System.Threading.Tasks;

namespace SmartDormitary.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}