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
        string Enqueue<T>(Expression<Func<T, Task>> methodCall);
        string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);
    }

    public class Job : IJob
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private IState _queue;

        public Job(IBackgroundJobClient backgroundJobClient)
        {
            _backgroundJobClient = backgroundJobClient;

            _queue = new EnqueuedState(JobQueues.DEFAULT);
        }

        public IJob OnQueue(string queue)
        {
            if (!JobQueues.Contains(queue))
            {
                throw new ArgumentException($"Queue is not defined: {queue}");
            }

            _queue = new EnqueuedState(queue.ToString());

            return this;
        }

        public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
        {
            return _backgroundJobClient.Create(methodCall, _queue);
        }

        public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt)
        {
            return _backgroundJobClient.Schedule(methodCall, enqueueAt);
        }
    }
}
