namespace Template.JobsBackgroundService
{
    internal class Constants
    {
        public class ParameterKeys
        {
            public const string JobConfigurationsKey = "JobConfigurations";
            public const string EnableBackgroundProcessKey = "EnableBackgroundProcess";
        }

        public class Errors
        {
            public const string NotImplementJobConfiguration = $"The '{{0}}' not implement interface '{nameof(IJobConfiguration)}'.";
        }

        public class CronConfigurations
        {
            public const string DefaultCron = "* * * ? * * *";
        }
    }
}
