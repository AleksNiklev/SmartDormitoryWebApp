using System;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using SmartDormitary.Data.Context;
using SmartDormitary.Services.Hubs.Service;
using SmartDormitory.API.DormitaryAPI;

namespace SmartDormitary.Services.Cron.Jobs
{
    public class JobService : IJobService
    {
        private readonly ISensorsAPI api;
        private readonly IHubService hubService;
        private readonly ISensorJob job;
        private readonly IServiceProvider serviceProvider;

        public JobService(IHubService hubService, IServiceProvider serviceProvider, ISensorsAPI api, ISensorJob job)
        {
            this.hubService = hubService;
            this.serviceProvider = serviceProvider;
            this.api = api;
            this.job = job;
        }

        public async void RunJob()
        {
            var scheduler = await StdSchedulerFactory.GetDefaultScheduler();

            await scheduler.Start();

            var dbContext = serviceProvider.CreateScope()
                .ServiceProvider
                .GetService<SmartDormitaryContext>();


            var job = JobBuilder.Create<SensorJob>()
                .WithIdentity("job1", "group1")
                .Build();

            job.JobDataMap["api"] = api;
            job.JobDataMap["dbContext"] = dbContext;
            job.JobDataMap["hubService"] = hubService;

            var trigger = TriggerBuilder.Create()
                .WithIdentity("trigger1", "group1")
                .StartNow()
                .WithSimpleSchedule(x => x
                    .WithIntervalInSeconds(20)
                    .RepeatForever())
                .Build();

            var exists = await scheduler.CheckExists(job.Key);

            if (!exists) await scheduler.ScheduleJob(job, trigger);
        }
    }
}