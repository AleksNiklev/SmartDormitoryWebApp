using FluentScheduler;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Cron.Contracts;
using SmartDormitary.Services.Extensions;
using SmartDormitary.Services.Hubs;
using SmartDormitory.API.DormitaryAPI;
using System;

namespace SmartDormitary.Services.Cron
{
    public class JobScheduleService : IJobScheduleService
    {
        private readonly ISensorsAPI api;
        private readonly IServiceProvider serviceProvider;
        private readonly IHubContext<NotifyHub> hubContext;

        public JobScheduleService(IServiceProvider serviceProvider, ISensorsAPI api, IHubContext<NotifyHub> hubContext)
        {
            this.api = api;
            this.serviceProvider = serviceProvider;
            this.hubContext = hubContext;
        }

        public void RunJobs()
        {
            JobManager.Initialize(new JobRegistry(this.api, this.serviceProvider, hubContext));
        }

        public void RunOneJob(Guid sensorId, int refreshTime)
        {
            JobManager.Initialize(new JobRegistry(this.api, this.serviceProvider, hubContext, sensorId, refreshTime));
        }
    }
}
