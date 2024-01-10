namespace Template.JobsBackgroundService
{
    public abstract class SingleJobServiceBase<T> : JobServiceBase<T> where T : ISingleJob
    {
        protected SingleJobServiceBase(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
