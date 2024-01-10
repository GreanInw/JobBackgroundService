using Quartz;

namespace Template.JobsBackgroundService
{
    public interface IJobConfiguration : ISortJob, IEnabledJob
    {
        JobKey JobKey { get; set; }
        CronExpression CronExpression { get; set; }
        Type ServiceType { get; set; }
    }
}