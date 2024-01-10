using Quartz;

namespace Template.JobsBackgroundService
{
    public class DefaultJobConfiguration : IJobConfiguration
    {
        public CronExpression CronExpression { get; set; }
        public int Sort { get; set; }
        public JobKey JobKey { get; set; }
        public bool Enabled { get; set; }
        public Type ServiceType { get; set; }
    }

}