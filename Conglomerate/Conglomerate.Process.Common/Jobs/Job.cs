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
        ///     Sets the id of the job that should finish before this job starts.
        /// </summary>
        /// <returns>The job that this method was called on.</returns>
        IJob WaitFor(string parentId);

        /// <summary>
        ///     Enqueues the job to be ran.
        /// </summary>
        /// <returns>The id of the job that was scheduled.</returns>
        string Enqueue<T>(Expression<Func<T, Task>> methodCall);

        /// <summary>
        ///     Schedules the job to be ran at the specified DateTimeOffset.
        ///     Note: The specified queue will not be respected. Scheduled jobs always go on the default queue.
        ///     Note: Will not wait for the another job to finish. Will always run at the time specified.
        /// </summary>
        /// <returns>The id of the job that was scheduled.</returns>
        string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt);
    }

    public class Job : IJob
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private IState _queue;
        private string _parentId;

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

        public IJob WaitFor(string parentId)
        {
            if (string.IsNullOrWhiteSpace(parentId))
            {
                throw new ArgumentException($"Cannot wait for an empty job id");
            }

            _parentId = parentId;

            return this;
        }

        public string Enqueue<T>(Expression<Func<T, Task>> methodCall)
        {
            return string.IsNullOrWhiteSpace(_parentId)
                ? _backgroundJobClient.Create(methodCall, _queue)
                : _backgroundJobClient.ContinueJobWith(_parentId, methodCall, _queue);
        }

        public string Schedule<T>(Expression<Func<T, Task>> methodCall, DateTimeOffset enqueueAt)
        {
            return _backgroundJobClient.Schedule(methodCall, enqueueAt);
        }
    }
}
