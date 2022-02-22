using Quartz;

namespace Scheduler
{
    /// <summary>
    /// Implementation IJob. Delegate gets from JobDataMap
    /// </summary>
    internal class SchedulerJob : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            var isAsyncAction = context.JobDetail.JobDataMap.ContainsKey("AsyncAction");

            if (isAsyncAction)
            {
                var asyncAction = context.JobDetail.JobDataMap["AsyncAction"] as Func<Task>;
                if (asyncAction != null)
                {
                    await asyncAction();
                }
            }
            else
            {
                var action = context.JobDetail.JobDataMap["Action"] as Action;
                action?.Invoke();
            }
        }
    }
}
