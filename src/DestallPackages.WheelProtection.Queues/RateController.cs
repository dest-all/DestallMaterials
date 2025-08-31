using DestallMaterials.Chronos;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues;

public class RateController : IRateController
{
    readonly IChronos _nowProvider;

    readonly CallConstraint[] _actionConstraints;

    readonly List<StartAndFinishTime> _history = new List<StartAndFinishTime>();

    readonly int _commonCallSlotsNumber;

    void StartAnother()
    {
        if (_subscriptions.TryDequeue(out var sub))
        {
            var nextCallAt = CalculateNextCallAllowedTime();
            _nowProvider.WhenComes(nextCallAt).ContinueWith((_) => sub.Item1());
        }
    }

    readonly ControlledQueue<Tuple<Action, Task>> _subscriptions
        = new(x => x.Item2.IsCompleted == false);

    readonly object _locker = new object();

    public RateController(IEnumerable<CallConstraint> actionConstraints, IChronos nowProvider)
    {
        _actionConstraints = actionConstraints.OptimizeConstraints().ToArray();
        _nowProvider = nowProvider;
        _commonCallSlotsNumber = _actionConstraints.Sum(a => a.MaxCallsCount);
    }

    public RateController(IEnumerable<CallConstraint> actionConstraints)
        : this(actionConstraints, RealTimeChronos.Instance)
    {
    }

    public ValueTask<IDisposable> AwaitNext(CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (TryTakeImmediately(out var immediateResult))
        {
            return new(immediateResult);
        }

        lock (_locker)
        {
            var tcs = new TaskCompletionSource<IDisposable>();

            cancellationToken.Register(() => tcs.TrySetCanceled());

            var action = () =>
            {
                if (tcs.Task.IsCompleted)
                {
                    return;
                }

                var result = CreateControlLocker();
                tcs.TrySetResult(result);
            };

            bool willBeNext = _subscriptions.FindThrough() is null;

            var subscriptionTask = Tuple.Create(action, tcs.Task as Task);
            _subscriptions.Enqueue(subscriptionTask);

            try
            {
                return new(tcs.Task);
            }
            finally
            {
                if (willBeNext)
                {
                    Task.Run(StartAnother, CancellationToken.None);
                }
            }
        }
    }

    public bool TryTakeImmediately(out IDisposable result)
    {
        lock (_locker)
        {
            var nextPossibleCallAt = CalculateNextCallAllowedTime();
            if (nextPossibleCallAt == default)
            {
                result = CreateControlLocker();
                return true;
            }

            result = null;
            return false;
        }
    }

    IDisposable CreateControlLocker()
    {
        var time = new StartAndFinishTime(_nowProvider.Now);
        _history.Add(time);
        return new RateControlLocker(() =>
        {
            lock (_locker)
            {
                time.Finish = _nowProvider.Now;
                StartAnother();
            }
        });
    }

    /// <summary>
    /// Calculate time in which the next call can be made.
    /// </summary>
    /// <returns>TimeSpan.MinValue - if call may be made immediately</returns>
    DateTimeOffset CalculateNextCallAllowedTime()
    {
        var executionsHistory = _history;
        var callsCount = executionsHistory.Count;
        Span<CallConstraint> actionConstraints = _actionConstraints;
        var now = _nowProvider.Now.UtcDateTime;
        if (executionsHistory.Count == 0)
        {
            return default;
        }

        DateTimeOffset earliestCallPossible = default;
        for (int i = 0; i < actionConstraints.Length; i++)
        {
            var (period, callsAllowedQuantity) = actionConstraints[i];
            var allowedNextCallByThisConstraint = DateTimeOffset.MaxValue;

            for (int callIndex = callsCount - 1; callsCount - callIndex < _commonCallSlotsNumber; callIndex--)
            {
                var (start, finish) = executionsHistory[callIndex];
                var isOver = now < finish;
                var withinConstraint = isOver || now - finish <= period;
                if (withinConstraint is false)
                {
                    allowedNextCallByThisConstraint = default;
                    break;
                }

                if (isOver)
                {
                    allowedNextCallByThisConstraint = start + period;
                    break;
                }
            }

            if (allowedNextCallByThisConstraint == DateTime.MaxValue)
            {
                return DateTime.MaxValue;
            }

            earliestCallPossible = earliestCallPossible > allowedNextCallByThisConstraint ?
                earliestCallPossible :
                allowedNextCallByThisConstraint;
        }

        return earliestCallPossible;
    }
}