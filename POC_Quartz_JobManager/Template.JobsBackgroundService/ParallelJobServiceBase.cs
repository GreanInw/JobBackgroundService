namespace Template.JobsBackgroundService
{
    public abstract class ParallelJobServiceBase<T> : JobServiceBase<T> where T : IParallelJob
    {
        protected ParallelJobServiceBase(IServiceProvider serviceProvider) : base(serviceProvider) { }
    }
}
