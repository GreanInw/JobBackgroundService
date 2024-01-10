using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl.Matchers;

namespace Template.JobsBackgroundService.JobManagements
{
    public abstract class DefaultJobManagementService<T> : JobServiceBase<T>, IJobManagementKeyMatcher
    {
        protected DefaultJobManagementService(IServiceProvider serviceProvider) : base(serviceProvider) { }

        public IMatcher<JobKey> Matcher => KeyMatcher<JobKey>.KeyEquals(JobKey);

        protected IEnumerable<IJobConfiguration> JobConfigurations
           => ServiceProvider.GetServices<IJobConfiguration>() ?? Enumerable.Empty<IJobConfiguration>();

        protected IBackgroundProcessConfiguration BackgroundProcessConfiguration
            => ServiceProvider.GetService<IBackgroundProcessConfiguration>()
                ?? new DefaultBackgroundProcessConfiguration();

        protected bool EnableBackgroundProcess => BackgroundProcessConfiguration.EnableBackgroundProcess;

        protected IEnumerable<IJobConfiguration> GetJobConfigurationsBy(IEnumerable<IJobKey> jobKeys)
        {
            foreach (var configuration in JobConfigurations)
            {
                if (!jobKeys.Any(w => w.JobKey.Equals(configuration.JobKey)))
                {
                    continue;
                }

                yield return configuration;
            }
        }
    }
}
