using DestallMaterials.WheelProtection.Extensions.Tasks;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues.QueueProcessors
{
    public abstract class InQueueAsynchronousProcessor<TPayload, TResult>
    {
        readonly ConcurrentQueue<RequestForProcession> _tasks
            = new ConcurrentQueue<RequestForProcession>();

        protected readonly Func<TPayload, Task<TResult>> _mainActionAsync;

        protected int _executedRequestsCount;

        protected List<DateTime> ExecutionsHistory { get; }

        protected readonly QueueConfiguration _configuration;

        private readonly IReadOnlyList<KeyValuePair<TimeSpan, int>> ActionConstraints;

        public InQueueAsynchronousProcessor(
            Func<TPayload, Task<TResult>> mainActionAsync,
            QueueConfiguration configuration)
        {
            _mainActionAsync = mainActionAsync;
            _configuration = configuration;
            ActionConstraints = configuration.ActionsConstraint.OptimizeConstraints();
            ExecutionsHistory = new List<DateTime>();
        }

        public Task<TResult> RequestForExecution(TPayload payload)
        {
            var taskSource = new TaskCompletionSource<TResult>();
            var task = taskSource.Task;

            var request = new RequestForProcession(payload, taskSource);

            _tasks.Enqueue(request);

            OnRequestAdded(request);

            return task.WithinDeadline(_configuration.ResponseAwaitingDeadline,
                $"Request hasn't been executed within time constraint of {_configuration.ResponseAwaitingDeadline.TotalMilliseconds} ms.");
        }

        private static readonly RequestsLimitReachedException RequestsLimitReachedException
            = new RequestsLimitReachedException();
        private static readonly RequestsLimitReachedException GuaranteedTimeoutException
            = new RequestsLimitReachedException("Timeout exception is guaranteed as the next allowed execution time will be too late.");

        protected async Task ExecuteRequestsAsync()
        {
            while (_tasks.TryDequeue(out var request))
            {
                var awaitTime = CalculateOverallAwaitTime();

                if (awaitTime > _configuration.ResponseAwaitingDeadline)
                {
                    request.TaskCompletion.SetException(GuaranteedTimeoutException);
                    continue;
                }

                var payload = request.Payload;

                await Task.Delay(awaitTime);

                ExecutionsHistory.Add(DateTime.UtcNow);
                ExecuteAnother(request, payload);
            }
        }

        private void ExecuteAnother(RequestForProcession request, TPayload payload)
        {
            _mainActionAsync(payload).ContinueWith(finishedAction =>
            {
                if (finishedAction.IsCompletedSuccessfully)
                {
                    request.TaskCompletion.TrySetResult(finishedAction.Result);
                }
                else
                {
                    request.TaskCompletion.TrySetException(finishedAction.Exception);
                }
            });
        }

        protected virtual TimeSpan CalculateOverallAwaitTime() => Algorithms.CalculateOverallAwaitTime(ActionConstraints, ExecutionsHistory);


        protected abstract void OnRequestAdded(RequestForProcession request);

        protected struct RequestForProcession
        {
            public RequestForProcession(TPayload payload, TaskCompletionSource<TResult> task)
            {
                Payload = payload;
                TaskCompletion = task;
            }

            public TPayload Payload { get; }
            public TaskCompletionSource<TResult> TaskCompletion { get; }
        }

    }
}
