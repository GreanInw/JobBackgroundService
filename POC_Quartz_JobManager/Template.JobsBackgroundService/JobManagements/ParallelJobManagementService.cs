using Microsoft.Extensions.DependencyInjection;
using Quartz;

namespace Template.JobsBackgroundService.JobManagements
{
    [DisallowConcurrentExecution]
    public class ParallelJobManagementService : DefaultJobManagementService<ParallelJobManagementService>, IJobManagementService
    {
        public ParallelJobManagementService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        protected IEnumerable<IParallelJob> Jobs => ServiceProvider.GetServices<IParallelJob>();

        public async Task Execute(IJobExecutionContext context)
        {
            var jobConfigurations = GetJobConfigurationsBy(Jobs);
            if (!EnableBackgroundProcess)
            {
                WriteLogInfo("=============> Delete Job for 'EnableBackgroundProcess' is 'false'.");
                await DeleteJobs(context, jobConfigurations.Select(s => s.JobKey));
                return;
            }

            if (!jobConfigurations.Any())
            {
                WriteLogInfo("=============> Job configuration not found.");
                return;
            }

            if (!Jobs.Any())
            {
                WriteLogInfo("=============> No Parallel Jobs...");
                return;
            }

            try
            {
                var addJobsConfigurations = jobConfigurations.Where(w => w.Enabled);
                if (addJobsConfigurations.Any())
                {
                    WriteLogInfo("Add or Replace Jobs and Trigger, Total : {Count}", addJobsConfigurations.Count());
                    await AddOrReplaceJobsTriggers(context, addJobsConfigurations);
                }

                var deleteJobsConfigurations = jobConfigurations.Where(w => !w.Enabled);
                if (deleteJobsConfigurations.Any())
                {
                    WriteLogInfo("Delete Jobs and Trigger, Total : {Count}", deleteJobsConfigurations.Count());
                    await DeleteJobs(context, deleteJobsConfigurations.Select(s => s.JobKey));
                }
            }
            catch (Exception ex)
            {
                WriteLogError(ex, ex.Message);
            }
        }

        protected async Task AddOrReplaceJobsTriggers(IJobExecutionContext context
            , IEnumerable<IJobConfiguration> jobConfigurations)
        {
            var jobsTriggers = new Dictionary<IJobDetail, IReadOnlyCollection<ITrigger>>();
            foreach (var item in jobConfigurations)
            {
                var jobDetail = CreateJobDetail(item.JobKey, item.ServiceType);
                var trigger = CreateTrigger(item.JobKey, item.CronExpression.CronExpressionString);
                jobsTriggers.Add(jobDetail, new List<ITrigger> { trigger });
            }

            await context.Scheduler.ScheduleJobs(jobsTriggers, true);
        }

        protected IJobDetail CreateJobDetail(JobKey jobKey, Type jobType)
            => JobBuilder.Create(jobType)
                .WithIdentity(jobKey)
                .Build();

        protected ITrigger CreateTrigger(JobKey jobKey, string cronExpression)
            => TriggerBuilder.Create()
                .ForJob(jobKey)
                .WithIdentity(jobKey.Name)
                .WithCronSchedule(cronExpression)
                .Build();

        protected async Task DeleteJobs(IJobExecutionContext context, IEnumerable<JobKey> jobKeys)
        {
            var deleteJobKeys = new List<JobKey>();
            foreach (var jobKey in jobKeys)
            {
                if (!await context.Scheduler.CheckExists(jobKey))
                {
                    continue;
                }
                deleteJobKeys.Add(jobKey);
            }

            await context.Scheduler.DeleteJobs(deleteJobKeys);
        }
    }
}
