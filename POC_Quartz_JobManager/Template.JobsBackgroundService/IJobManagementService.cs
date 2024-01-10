using Quartz;

namespace Template.JobsBackgroundService
{
    public interface IJobManagementService : IJob, IJobKey, IJobManagementKeyMatcher { }
}
