using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Quartz;
using Template.JobsBackgroundService.JobManagements;

namespace Template.JobsBackgroundService.Listeners
{
    [DisallowConcurrentExecution]
    public class JobManagementListenerService : IJobListener, IJobKey
    {
        private readonly IServiceProvider _serviceProvider;

        public JobManagementListenerService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public string Name => typeof(JobManagementListenerService).FullName;
        public JobKey JobKey => new(Name);

        protected IEnumerable<Type> JobManagementTypes
            => new[] { typeof(SingleJobManagementService), typeof(ParallelJobManagementService) };

        public Task JobExecutionVetoed(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            return Task.CompletedTask;
        }

        public Task JobToBeExecuted(IJobExecutionContext context, CancellationToken cancellationToken = default)
        {
            if (!JobManagementTypes.Any(w => w == context.JobInstance.GetType()))
            {
                return Task.CompletedTask;
            }

            //Update status, start execute date before execute job.

            return Task.CompletedTask;
        }

        public Task JobWasExecuted(IJobExecutionContext context, JobExecutionException jobException, CancellationToken cancellationToken = default)
        {
            //Update status, end execute date after execute job.

            return Task.CompletedTask;
        }
    }
}
