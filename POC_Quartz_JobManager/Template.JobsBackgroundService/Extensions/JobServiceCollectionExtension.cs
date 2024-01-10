using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Template.JobsBackgroundService.Helpers;
using Template.JobsBackgroundService.JobManagements;
using Template.JobsBackgroundService.Listeners;

namespace Template.JobsBackgroundService.Extensions
{
    public static class JobServiceCollectionExtension
    {
        public static WebApplicationBuilder AddBackgroundServiceWithQuartz(this WebApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Services.AddQuartzHostedService(f => f.WaitForJobsToComplete = true);
            applicationBuilder.Services.AddQuartz(configure =>
            {
                configure.AddJob<SingleJobManagementService>(SingleJobManagementService.JobKeyInstance)
                    .AddTrigger(trigger =>
                    {
                        trigger.ForJob(SingleJobManagementService.JobKeyInstance)
                            .WithCronSchedule(Constants.CronConfigurations.DefaultCron);
                    });

                configure.AddJob<ParallelJobManagementService>(ParallelJobManagementService.JobKeyInstance)
                    .AddTrigger(trigger =>
                    {
                        trigger.ForJob(ParallelJobManagementService.JobKeyInstance)
                            .WithCronSchedule(Constants.CronConfigurations.DefaultCron);
                    });

                //Get "IMatcher<JobKey>" from "IJobManagementKeyMatcher"
                var matchers = JobServiceHelper.GetJobManagementKeyMatchersFromCurrentAssembly()
                    .Select(s => s.Matcher).ToArray();

                configure.AddJobListener<JobManagementListenerService>(matchers);
            });
            return applicationBuilder;
        }

        public static IServiceCollection AddJobBackgroundServiceOptions(this IServiceCollection services
            , JobBackgroundServiceOptions options)
        {
            if (options is IBackgroundProcessConfiguration)
            {
                var defaultConfiguration = new DefaultBackgroundProcessConfiguration
                {
                    EnableBackgroundProcess = options.EnableBackgroundProcess
                };
                services.AddSingleton(typeof(IBackgroundProcessConfiguration), defaultConfiguration);
            }

            return services;
        }

        public static IServiceCollection AddJobConfigurationsScoped(this IServiceCollection services, Type implementationType)
            => services.AddScoped(typeof(IJobConfiguration), implementationType);

        public static IServiceCollection AddJobConfigurationsScoped<T>(this IServiceCollection services) where T : IJobConfiguration
            => services.AddJobConfigurationsScoped(typeof(T));

        public static IServiceCollection AddJobConfigurationsScoped(this IServiceCollection services, IEnumerable<Type> implementationTypes)
        {
            foreach (var type in implementationTypes)
            {
                services.AddJobConfigurationsScoped(type);
            }

            return services;
        }

        public static IServiceCollection AddJobConfigurationsSingleton(this IServiceCollection services, Type implementationType)
            => services.AddSingleton(typeof(IJobConfiguration), implementationType);

        public static IServiceCollection AddJobConfigurationsSingleton<T>(this IServiceCollection services) where T : IJobConfiguration
            => services.AddJobConfigurationsSingleton(typeof(T));

        public static IServiceCollection AddJobConfigurationsSingleton(this IServiceCollection services, IEnumerable<Type> implementationTypes)
        {
            foreach (var type in implementationTypes)
            {
                services.AddJobConfigurationsSingleton(type);
            }

            return services;
        }

    }
}
