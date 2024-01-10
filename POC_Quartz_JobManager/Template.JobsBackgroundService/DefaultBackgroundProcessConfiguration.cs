namespace Template.JobsBackgroundService
{
    public class DefaultBackgroundProcessConfiguration : IBackgroundProcessConfiguration
    {
        public bool EnableBackgroundProcess { get; set; } = false;
    }
}