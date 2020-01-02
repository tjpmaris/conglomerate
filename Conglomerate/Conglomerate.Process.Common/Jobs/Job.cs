using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Conglomerate.Process.Common.Constants;

using Hangfire;
using Hangfire.States;

namespace Conglomerate.Process.Common.Jobs
{
    public interface IJob
    {
        IJob OnQueue(string queue);
        string Start<T>(Expression<Func<T, Task>> methodCall);
    }

    public class Job : IJob
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private IState state;

        public Job(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;

            state = new EnqueuedState(JobQueues.DEFAULT);
        }

        public IJob OnQueue(string queue)
        {
            if (!JobQueues.Contains(queue))
            {
                throw new ArgumentException($"Queue is not defined: {queue}");
            }

            state = new EnqueuedState(queue.ToString());

            return this;
        }

        public string Start<T>(Expression<Func<T, Task>> methodCall)
        {
            return _backgroundJobClient.Create(methodCall, state);
        }
    }
}
