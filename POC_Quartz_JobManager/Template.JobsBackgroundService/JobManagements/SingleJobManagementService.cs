using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Template.JobsBackgroundService.JobManagements
{
    [DisallowConcurrentExecution]
    public class SingleJobManagementService : DefaultJobManagementService<SingleJobManagementService>, IJobManagementService
    {
        public SingleJobManagementService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected IEnumerable<ISingleJob> Jobs => ServiceProvider.GetServices<ISingleJob>();

        public async Task Execute(IJobExecutionContext context)
        {
            var jobConfigurations = GetJobConfigurationsBy(Jobs);
            if (!EnableBackgroundProcess)
            {
                WriteLogInfo("=============> The 'EnableBackgroundProcess' is 'false'.");
                return;
            }
            
            if (!jobConfigurations.Any())
            {
                WriteLogInfo("=============> Job configuration not found.");
                return;
            }

            if (!Jobs.Any())
            {
                WriteLogInfo("=============> No Single Jobs..");
                return;
            }

            try
            {
                //Execute jobs
                await ExecuteJobsAsync(context, jobConfigurations);
            }
            catch (Exception ex)
            {
                WriteLogError(ex, ex.Message);
            }
        }

        private async Task ExecuteJobsAsync(IJobExecutionContext context, IEnumerable<IJobConfiguration> jobConfigurations)
        {
            foreach (var configuration in jobConfigurations.OrderBy(o => o.Sort))
            {
                if (!configuration.Enabled)
                {
                    WriteLogInfo("=============> Job : {0} disabled.", configuration.JobKey.Name);
                    continue;
                }

                var job = Jobs.FirstOrDefault(f => f.JobKey.Equals(configuration.JobKey));
                if (job is null)
                {
                    WriteLogInfo("=============> Job : {0} not registered(DI).", configuration.JobKey.Name);
                    continue;
                }

                //Job it's not time yet.
                if (!configuration.CronExpression.IsSatisfiedBy(DateTimeOffset.UtcNow))
                {
                    continue;
                }

                WriteLogInfo("=============> Start executing job '{0}', Date : {1}", job.JobKey.Name, DateTime.Now);
                await job.ExecuteAsync(context);
                WriteLogInfo("=============> End executed job '{0}', Date : {1}", job.JobKey.Name, DateTime.Now);
            }
        }
    }
}
