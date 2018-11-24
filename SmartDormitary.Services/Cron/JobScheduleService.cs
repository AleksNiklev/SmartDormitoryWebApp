using FluentScheduler;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Cron.Contracts;
using SmartDormitary.Services.Extensions;
using SmartDormitory.API.DormitaryAPI;
using System;

namespace SmartDormitary.Services.Cron
{
    public class JobScheduleService : IJobScheduleService
    {
        private readonly ISensorsAPI api;
        private readonly IServiceProvider serviceProvider;

        public JobScheduleService(IServiceProvider serviceProvider, ISensorsAPI api)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
        }

        public void RunJobs()
        {
            JobManager.Initialize(new JobRegistry(this.api, this.serviceProvider));
        }

        public void RunOneJob(Guid sensorId, int refreshTime)
        {
            JobManager.Initialize(new JobRegistry(this.api, this.serviceProvider, sensorId, refreshTime));
        }
    }
}
