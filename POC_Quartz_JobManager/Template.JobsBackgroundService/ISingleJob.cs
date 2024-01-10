using Quartz;

namespace Template.JobsBackgroundService
{
    public interface ISingleJob : IJobKey
    {
        Task ExecuteAsync(IJobExecutionContext context);
    }
}