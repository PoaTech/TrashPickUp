using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iTrash.Jobs
{
    public static class JobScheduler
    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler();
            scheduler.Start();

            IJobDetail job = JobBuilder.Create<DailyJob>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                .WithCronSchedule("0 0 0 * * ?", x => x.WithMisfireHandlingInstructionFireAndProceed())
                .Build();

            scheduler.ScheduleJob(job, trigger);
        }
    }
}