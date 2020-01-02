using Hangfire;

namespace Conglomerate.Process.Common.Jobs
{
    public interface IJobFactory
    {
        ISingleJob CreateSingleJob();
    }

    public class JobFactory : IJobFactory
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobFactory(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public ISingleJob CreateSingleJob()
        {
            return new SingleJob(_backgroundJobClient);
        }
    }
}
