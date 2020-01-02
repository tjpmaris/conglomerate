using Hangfire;

namespace Conglomerate.Process.Common.Jobs
{
    public interface IJobFactory
    {
        IJob CreateJob();
    }

    public class JobFactory : IJobFactory
    {
        private readonly IBackgroundJobClient _backgroundJobClient;

        public JobFactory(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;
        }

        public IJob CreateJob()
        {
            return new Job(_backgroundJobClient);
        }
    }
}
