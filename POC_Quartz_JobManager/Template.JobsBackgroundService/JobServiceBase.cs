using Microsoft.Extensions.Logging;
using Quartz;
using Template.JobsBackgroundService.Helpers;

namespace Template.JobsBackgroundService
{
    public abstract class JobServiceBase : IJobKey
    {
        public static Type[] ImplementInterfaceTypes => new Type[]
        {
            typeof(IJobManagementService), typeof(ISingleJob), typeof(IParallelJob)
        };

        public JobServiceBase(Type type, IServiceProvider serviceProvider)
        {
            if (type is null) throw new ArgumentNullException(nameof(type));
            if (serviceProvider is null) throw new ArgumentNullException(nameof(serviceProvider));

            if (!IsExistInterfaces(type))
            {
                string errorMessage = string.Format("The type '{0}' not support. The type '{0}' must have implement : {1}."
                    , type.FullName
                    , string.Join(",", ImplementInterfaceTypes.Select(s => s.FullName)));

                throw new Exception(errorMessage);
            }

            Type = type;
            ServiceProvider = serviceProvider;
        }

        public JobKey JobKey => JobKeyHelper.CreateInstance(Type);

        protected Type Type { get; }
        protected IServiceProvider ServiceProvider { get; }
        protected ILogger Logger => JobServiceHelper.CreateLoggerConsole(Type);

        internal static ILoggerFactory CreateLoggerFactory()
            => LoggerFactory.Create(builder => builder.AddConsole());

        internal static bool IsExistInterfaces(Type type)
            => type.GetInterfaces().Any(w => ImplementInterfaceTypes.Contains(w));

        protected void WriteLogInfo(string message)
            => Logger.LogInformation(message);

        protected void WriteLogInfo(string message, params object[] args)
            => Logger.LogInformation(message, args);

        protected void WriteLogError(Exception exception, string message = null)
            => Logger.LogError(exception, message ?? exception.Message);
    }

    public abstract class JobServiceBase<T> : JobServiceBase
    {
        public static JobKey JobKeyInstance => JobKeyHelper.CreateInstance(typeof(T));

        protected JobServiceBase(IServiceProvider serviceProvider) : base(typeof(T), serviceProvider)
        { }
    }
}
