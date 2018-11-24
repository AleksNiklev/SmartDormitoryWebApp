using System;

namespace SmartDormitary.Services.Cron.Contracts
{
    public interface IJobScheduleService
    {
        void RunJobs();
        void RunOneJob(Guid sensorId, int refreshTime);
    }
}