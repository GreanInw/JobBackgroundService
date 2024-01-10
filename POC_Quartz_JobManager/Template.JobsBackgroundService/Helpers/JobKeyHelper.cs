using Quartz;

namespace Template.JobsBackgroundService.Helpers
{
    internal sealed class JobKeyHelper
    {
        public static JobKey CreateInstance(Type type) => new(type.FullName);

        public static JobKey CreateInstance<T>() => CreateInstance(typeof(T));

    }
}
