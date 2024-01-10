using Microsoft.Extensions.Logging;
using Quartz;
using System.Reflection;

namespace Template.JobsBackgroundService.Helpers
{
    internal sealed class JobServiceHelper
    {
        public static Assembly CurrentAssembly => typeof(JobServiceHelper).Assembly;

        public static IEnumerable<IJobManagementKeyMatcher> GetJobManagementKeyMatchers(Assembly assembly)
        {
            foreach (var item in assembly.GetTypes())
            {
                var interfaceType = item.GetInterface(nameof(IJobManagementKeyMatcher), true);
                if (interfaceType == null || interfaceType is not IJobManagementKeyMatcher newInterfaceType)
                {
                    continue;
                }

                yield return newInterfaceType;
            }
        }

        public static IEnumerable<IJobManagementKeyMatcher> GetJobManagementKeyMatchersFromCurrentAssembly()
            => GetJobManagementKeyMatchers(CurrentAssembly);

        public static ILoggerFactory CreateLoggerFactoryConsole()
            => LoggerFactory.Create(builder => builder.AddConsole());

        public static ILogger CreateLoggerConsole(Type type)
            => CreateLoggerFactoryConsole().CreateLogger(type.FullName);

        public static ILogger<T> CreateLoggerConsole<T>()
            => CreateLoggerFactoryConsole().CreateLogger<T>();

        public static Type GetJobTypeFromCurrentyAssembly(string name)
            => string.IsNullOrWhiteSpace(name)
                ? throw new ArgumentNullException(nameof(name))
                : CurrentAssembly.GetType(name);

        public static ITrigger CreateTrigger(JobKey jobKey, string cronExpression)
            => TriggerBuilder.Create().WithIdentity(jobKey.Name)
                .ForJob(jobKey)
                .WithCronSchedule(cronExpression)
                .Build();

    }
}
