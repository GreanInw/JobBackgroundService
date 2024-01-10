using Quartz;

namespace Template.JobsBackgroundService
{
    public interface IParallelJob : IJob, IJobKey { }
}