using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace POC_Quartz_JobManager.Entities
{
    public class JobSchedule : BaseEntityCosmos
    {
        [JsonConverter(typeof(StringEnumConverter))]
        public JobScheduleType JobScheduleType { get; set; }
        public JobScheduleSettingsEntity JobScheduleSettings { get; set; }
        public JobProcessEntity[] JobProcesses { get; set; } = Array.Empty<JobProcessEntity>();

        public class JobScheduleSettingsEntity
        {
            public bool EnableBackgroundProcess { get; set; }
            public JobScheduleConfigurationEntity[] Configurations { get; set; } = Array.Empty<JobScheduleConfigurationEntity>();
        }

        public class JobScheduleConfigurationEntity
        {
            public string TypeName { get; set; }
            public bool Enable { get; set; }
            public int Sort { get; set; }
            public string CronExpression { get; set; }
        }

        public class JobProcessEntity
        {
            public string TypeName { get; set; }
            public bool IsProcessing { get; set; }
            public DateTime StartDate { get; set; }
            public DateTime? EndDate { get; set; }
        }

    }

    public enum JobScheduleType
    {
        Settings, History
    }

    public abstract class BaseEntityCosmos
    {
        [JsonProperty(PropertyName = "id")]
        public virtual string Id { get; set; }

        [JsonProperty(PropertyName = "partitionKey")]
        public virtual string PartitionKey { get; set; } = null!;
        [JsonProperty(PropertyName = "createdDate")]
        public virtual DateTime? CreatedDate { get; set; }
        [JsonProperty(PropertyName = "createdBy")]
        public virtual string CreatedBy { get; set; } = null!;
    }

}
