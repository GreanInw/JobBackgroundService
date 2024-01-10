using Quartz;
using Template.JobsBackgroundService;

namespace POC_Quartz_JobManager.JobServices
{
    public class FirstJobService : SingleJobServiceBase<FirstJobService>, ISingleJob
    {
        public FirstJobService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            Logger.LogInformation($"============================ Start :: {nameof(FirstJobService)}");
            for (int i = 0; i < 10; i++)
            {
                Logger.LogInformation("============================ Execute {1}, Count '{0}'"
                    , nameof(FirstJobService), i);
                await Task.Delay(1000);
            }

            Logger.LogInformation($"============================ End :: {nameof(FirstJobService)}");
        }
    }

    public class SecondJobService : SingleJobServiceBase<SecondJobService>, ISingleJob
    {
        public SecondJobService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        public async Task ExecuteAsync(IJobExecutionContext context)
        {
            Logger.LogInformation($"************************** Start :: {nameof(SecondJobService)}");
            for (int i = 0; i < 10; i++)
            {
                Logger.LogInformation("************************** Execute {1}, Count '{0}'"
                    , nameof(SecondJobService), i);
                await Task.Delay(1000);
            }

            Logger.LogInformation($"************************** End :: {nameof(SecondJobService)}");
        }
    }

}
