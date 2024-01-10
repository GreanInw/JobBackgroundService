using Quartz;

namespace Template.JobsBackgroundService.JobServices
{
    [DisallowConcurrentExecution]
    public class ParallelFirstJobService : ParallelJobServiceBase<ParallelFirstJobService>, IParallelJob
    {
        public ParallelFirstJobService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        public async Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"============================ Start :: {nameof(ParallelFirstJobService)}");
            for (int i = 0; i < 30; i++)
            {
                Logger.LogInformation("============================ Execute {1}, Count '{0}'"
                    , nameof(ParallelFirstJobService), i);
                await Task.Delay(1000);
            }

            Logger.LogInformation($"============================ End :: {nameof(ParallelFirstJobService)}");
        }
    }

    [DisallowConcurrentExecution]
    public class ParallelSecondJobService : ParallelJobServiceBase<ParallelFirstJobService>, IParallelJob
    {
        public ParallelSecondJobService(IServiceProvider serviceProvider) : base(serviceProvider)
        { }

        public async Task Execute(IJobExecutionContext context)
        {
            Logger.LogInformation($"============================ Start :: {nameof(ParallelSecondJobService)}");
            for (int i = 0; i < 30; i++)
            {
                Logger.LogInformation("============================ Execute {1}, Count '{0}'"
                    , nameof(ParallelSecondJobService), i);
                await Task.Delay(1000);
            }

            Logger.LogInformation($"============================ End :: {nameof(ParallelSecondJobService)}");
        }
    }
}
