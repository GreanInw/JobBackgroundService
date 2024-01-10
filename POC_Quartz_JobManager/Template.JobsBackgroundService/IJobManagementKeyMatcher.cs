using Quartz;

namespace Template.JobsBackgroundService
{
    public interface IJobManagementKeyMatcher
    {
        IMatcher<JobKey> Matcher { get; }
    }
}
