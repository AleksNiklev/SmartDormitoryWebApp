using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using SmartDormitary.Data.Models;
using SmartDormitary.Services.Contracts;
using SmartDormitary.Services.Hubs;
using SmartDormitory.API.DormitaryAPI;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using SmartDormitary.Data.Context;
using System.Linq;
using System.Diagnostics;
using Quartz;
using Quartz.Impl;
using SmartDormitary.Services.Hubs.Service;

namespace SmartDormitary.Services.Cron.Jobs
{
    public class JobService : IJobService
    {
        private readonly IHubService hubService;
        private readonly IServiceProvider serviceProvider;
        private readonly ISensorsAPI api;
        private readonly ISensorJob job;

        public JobService(IHubService hubService, IServiceProvider serviceProvider, ISensorsAPI api, ISensorJob job)
        {
            this.hubService = hubService;
            this.serviceProvider = serviceProvider;
            this.api = api;
            this.job = job;
        }

        public async void RunJob()
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            var dbContext = serviceProvider.CreateScope()
                            .ServiceProvider
                            .GetService<SmartDormitaryContext>();


            IJobDetail job = JobBuilder.Create<SensorJob>()
                .WithIdentity("job1", "group1")
                .Build();

            job.JobDataMap["api"] = this.api;
            job.JobDataMap["dbContext"] = dbContext;
            job.JobDataMap["hubService"] = this.hubService;

            ITrigger trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(20)
                    .RepeatForever())
                .Build();

            var exists = await scheduler.CheckExists(job.Key);

            if (!exists)
            {
                await scheduler.ScheduleJob(job, trigger);
            }
        }
    }
}
