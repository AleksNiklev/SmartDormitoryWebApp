using System.Threading.Tasks;
using Quartz;

namespace SmartDormitary.Services.Cron.Jobs
{
    public interface ISensorJob
    {
        Task Execute(IJobExecutionContext context);
    }
}