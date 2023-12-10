using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues
{
    public interface IRateController
    {
        Task<IDisposable> WaitNext();
    }

    public class RateController : IRateController
    {
        public class RateControlLocker : IDisposable
        {
            private readonly Action _onDisposed;

            public RateControlLocker(Action onDisposed)
            {
                _onDisposed = onDisposed;
            }

            public void Release()
            {
                _onDisposed();
            }

            void IDisposable.Dispose() => Release();
        }
        readonly IReadOnlyList<KeyValuePair<TimeSpan, int>> _actionConstraints;

        readonly List<StartAndFinishTime> _history = new List<StartAndFinishTime>();

        void OneIsDone()
        {
            if (_subscriptions.TryDequeue(out var sub))
            {
                sub();
            }
        }

        readonly ConcurrentQueue<Action> _subscriptions = new ConcurrentQueue<Action>();

        readonly object _locker = new object();

        public RateController(IEnumerable<KeyValuePair<TimeSpan, int>> actionConstraints)
        {
            _actionConstraints = actionConstraints.OptimizeConstraints();
        }

        public Task<IDisposable> WaitNext()
        {
            lock (_locker)
            {
                var awaitTime = CalculateAwaitTime();

                if (awaitTime == TimeSpan.MinValue)
                {
                    var taskSource = new TaskCompletionSource<IDisposable>();

                    _subscriptions.Enqueue(() =>
                    {
                        var result = CreateNewControlLocker();
                        taskSource.SetResult(result);
                    });

                    return taskSource.Task;
                }
                return Task.Delay(awaitTime).ContinueWith(t => CreateNewControlLocker());
            }
        }

        IDisposable CreateNewControlLocker()
        {
            var time = new StartAndFinishTime()
            {
                Start = DateTime.UtcNow
            };
            _history.Add(time);
            return new RateControlLocker(() =>
            {
                lock (_locker)
                {
                    time.Finish = DateTime.UtcNow;
                    OneIsDone();
                }
            });
        }

        public class StartAndFinishTime
        {
            public DateTime Start;
            public DateTime Finish;

            public TimeSpan Duration => (Start > default(DateTime) && Finish > default(DateTime)) ? Finish - Start : default;
        }

        TimeSpan CalculateAwaitTime()
        {
            var executionsHistory = _history;
            var actionConstraints = _actionConstraints;
            var result = TimeSpan.Zero;

            int currentlyRunning = 0;

            if (executionsHistory.Count == 0)
            {
                return result;
            }

            var currentTime = DateTime.UtcNow;

#if NETSTANDARD2_0
            var firstCalls = new DateTime[actionConstraints.Count];
            var actionsInLimits = new int[actionConstraints.Count];      
#else
            Span<DateTime> firstCalls = stackalloc DateTime[actionConstraints.Count];
            Span<int> actionsInLimits = stackalloc int[actionConstraints.Count];

#endif

            for (var historyPointIndex = executionsHistory.Count - 1; historyPointIndex >= 0; historyPointIndex--)
            {
                var actionDate = executionsHistory[historyPointIndex];
                if (actionDate.Finish == default)
                {
                    currentlyRunning++;
                }

                for (var constraintIndex = actionConstraints.Count - 1; constraintIndex >= 0; constraintIndex--)
                {
                    var constraint = actionConstraints[constraintIndex];

                    if (constraint.Value <= currentlyRunning)
                    {
                        return TimeSpan.MinValue;
                    }

                    var withinConstraint = actionDate.Finish == default || currentTime - actionDate.Start < constraint.Key;

                    if (withinConstraint)
                    {
                        actionsInLimits[constraintIndex]++;
                        if (actionsInLimits[constraintIndex] == constraint.Value && actionDate.Finish != default)
                        {
                            firstCalls[constraintIndex] = actionDate.Finish;
                            break;
                        }
                    }
                }
            }

            bool needsCleaning = executionsHistory.Count > actionConstraints[0].Value * 2;
            if (needsCleaning)
            {
                executionsHistory.RemoveAll(e => actionConstraints.All(ac => DateTime.UtcNow - e.Finish > ac.Key));
            }

            var toAwait = TimeSpan.Zero;

            for (var i = 0; i < firstCalls.Length; i++)
            {
                var call = firstCalls[i];

                if (call == default)
                {
                    continue;
                }

                var timeLengthForConstraint = actionConstraints[i].Key;

                var toAwaitForThisConstraint = timeLengthForConstraint - (currentTime - call);

                if (toAwaitForThisConstraint > toAwait)
                {
                    toAwait = toAwaitForThisConstraint;
                }
            }

            return toAwait;
        }
    }
}
