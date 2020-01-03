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
        /// <summary>
        ///     Sets the queue for the job.
        ///     Note: Will throw an exception if the queue is not defined in JobQueues.
        /// </summary>
        /// <returns>The job that this method was called on.</returns>
        IJob OnQueue(string queue);

        /// <summary>
        ///     Enqueues the job to be ran.
        /// </summary>
        /// <returns>The id of the job that was scheduled.</returns>
        string Enqueue<T>(Expression<Func<T, Task>> methodCall);

        /// <summary>
        ///     Schedules the job to be ran at the specified DateTimeOffset.
        ///     Note: The specified queue will not be respected. Scheduled jobs always go on the default queue
        /// </summary>
        /// <returns>The id of the job that was scheduled.</returns>
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
