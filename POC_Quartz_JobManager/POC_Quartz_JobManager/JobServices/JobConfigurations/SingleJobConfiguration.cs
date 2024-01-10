using Quartz;
using Template.JobsBackgroundService;

namespace POC_Quartz_JobManager.JobServices.JobConfigurations
{
    public class FirstJobConfiguration : IJobConfiguration
    {
        public FirstJobConfiguration()
        {
            JobKey = FirstJobService.JobKeyInstance;
            CronExpression = new CronExpression("*/10 * * ? * * *");
            ServiceType = typeof(FirstJobService);
        }

        public JobKey JobKey { get; set; }
        public CronExpression CronExpression { get; set; }
        public Type ServiceType { get; set; }
        public int Sort { get; set; } = 1;
        public bool Enabled { get; set; } = true;
    }

    public class SecondJobConfiguration : IJobConfiguration
    {
        public SecondJobConfiguration()
        {
            JobKey = SecondJobService.JobKeyInstance;
            CronExpression = new CronExpression("*/30 * * ? * * *");
            ServiceType = typeof(SecondJobService);
        }

        public JobKey JobKey { get; set; }
        public CronExpression CronExpression { get; set; }
        public Type ServiceType { get; set; }
        public int Sort { get; set; } = 2;
        public bool Enabled { get; set; } = true;
    }
}
