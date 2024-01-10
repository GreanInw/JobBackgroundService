using Quartz;

namespace Template.JobsBackgroundService
{
    public interface IJobKey
    {
        JobKey JobKey { get; }
    }
}
