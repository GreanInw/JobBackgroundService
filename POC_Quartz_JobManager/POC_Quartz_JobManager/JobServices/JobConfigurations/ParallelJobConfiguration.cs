using Quartz;
using Template.JobsBackgroundService;
using Template.JobsBackgroundService.JobServices;

namespace POC_Quartz_JobManager.JobServices.JobConfigurations
{
    public class ParallelFirstJobConfiguration : IJobConfiguration
    {
        public ParallelFirstJobConfiguration()
        {
            JobKey = ParallelFirstJobService.JobKeyInstance;
            CronExpression = new CronExpression("*/10 * * ? * * *");
            ServiceType = typeof(ParallelFirstJobService);
        }

        public JobKey JobKey { get; set; }
        public CronExpression CronExpression { get; set; }
        public Type ServiceType { get; set; }
        public int Sort { get; set; } = 1;
        public bool Enabled { get; set; } = true;
    }

    public class ParallelSecondJobConfiguration : IJobConfiguration
    {
        public ParallelSecondJobConfiguration()
        {
            JobKey = ParallelSecondJobService.JobKeyInstance;
            CronExpression = new CronExpression("*/15 * * ? * * *");
            ServiceType = typeof(ParallelSecondJobService);
        }

        public JobKey JobKey { get; set; }
        public CronExpression CronExpression { get; set; }
        public Type ServiceType { get; set; }
        public int Sort { get; set; } = 2;
        public bool Enabled { get; set; } = true;
    }
}
