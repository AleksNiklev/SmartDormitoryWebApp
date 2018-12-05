using System.Threading.Tasks;

namespace SmartDormitary.Services.Hubs.Service
{
    public interface IHubService
    {
        Task Notify(string userId, string name, string value, string measurementType);
    }
}