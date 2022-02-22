using Quartz;
using Quartz.Impl;

namespace Scheduler
{
    /// <summary>
    /// Adding scheduler to your app with cron expression
    /// </summary>
    public class Scheduler
    {
        private readonly IScheduler _scheduler;

        public Scheduler()
        {
            StdSchedulerFactory factory = new StdSchedulerFactory();
            _scheduler = factory.GetScheduler().GetAwaiter().GetResult();
            _scheduler.Start().GetAwaiter().GetResult();
        }

        private string GetJobName() => "customJob#" + new Random().Next();

        /// <summary>
        /// Add custom job with CRON schedule
        /// </summary>
        public void AddJob(Action action, string cronExp)
        {
            var jobData = new JobDataMap();
            jobData.Add("Action", action);

            var jobName = GetJobName();

            var job = JobBuilder.Create<SchedulerJob>()
                .UsingJobData(jobData)
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(jobName + "Trigger")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule(cronExp)
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }

        /// <summary>
        /// Add custom async job with CRON schedule
        /// </summary>
        public void AddAsyncJob(Func<Task> task, string cronExp)
        {
            var jobData = new JobDataMap();
            jobData.Add("AsyncAction", task);

            var jobName = GetJobName();

            var job = JobBuilder.Create<SchedulerJob>()
                .UsingJobData(jobData)
                .WithIdentity(jobName)
                .Build();

            var trigger = TriggerBuilder.Create()
                .WithIdentity(jobName + "Trigger")
                .ForJob(job)
                .StartNow()
                .WithCronSchedule(cronExp)
                .Build();

            _scheduler.ScheduleJob(job, trigger);
        }
    }
}
