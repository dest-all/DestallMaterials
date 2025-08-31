using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DestallMaterials.Chronos
{
    public class ManualChronos : IChronos, IChronosControll
    {
        decimal _relativeSpeed = 1;
        readonly List<ChronosCallback> _onTimeChanged
            = new List<ChronosCallback>();

        Func<DateTimeOffset> _getNow;

        CancellationTokenSource _timeFlowStopping;

        TimeSpan _realTimeOffset;
        DateTimeOffset _baseTime;

        void AttuneTimeFlow()
        {
            if (_relativeSpeed == 0)
            {
                return;
            }

            var oldTimeFlowStopping = _timeFlowStopping;

            var shortestCallback = _onTimeChanged
                .OrderBy(tc => tc.AssociatedTime)
                .FirstOrDefault();

            var timePace = shortestCallback != null ?
                TimeSpan.FromTicks((long)((shortestCallback.AssociatedTime - Now).Ticks / 10 / _relativeSpeed)) :
                TimeSpan.Zero;

            if (timePace == TimeSpan.Zero)
            {
                return;
            }

            var newTimeFlowStopping = new CancellationTokenSource();

            Action newTracker = () =>
                Task.Run(async () =>
                {
                    while (newTimeFlowStopping.IsCancellationRequested == false)
                    {
                        await Task.Delay(timePace, newTimeFlowStopping.Token);
                        CheckCallbacks();

                        if (_onTimeChanged.Count == 0)
                        {
                            break;
                        }
                    }
                }
             );

            if (oldTimeFlowStopping != null)
            {
                oldTimeFlowStopping.Token.Register(newTracker);
                oldTimeFlowStopping?.Cancel();
            }
            else
            {
                newTracker();
            }

            _timeFlowStopping = newTimeFlowStopping;

        }

        public ManualChronos() : this(0)
        {
        }

        public ManualChronos(Func<DateTimeOffset> realTimeSource)
            : this(realTimeSource(), 0, realTimeSource)
        {
        }

        public ManualChronos(
            DateTimeOffset initialTime,
            decimal relativeSpeed,
            Func<DateTimeOffset> realTimeSource)
        {
            var realNow = realTimeSource();
            _relativeSpeed = relativeSpeed;
            _baseTime = realNow;
            _realTimeOffset = initialTime - _baseTime;
            _getNow = realTimeSource;
        }

        static Func<DateTimeOffset> CreateStopwatchSource()
        {
            var baseTime = DateTimeOffset.Now;
            var stopwatch = Stopwatch.StartNew();

            return () => baseTime + stopwatch.Elapsed;
        }

        public ManualChronos(decimal relativeSpeed)
            : this(DateTimeOffset.Now, relativeSpeed, CreateStopwatchSource())
        {
        }

        public ManualChronos(DateTimeOffset initialTime, decimal relativeSpeed)
            : this(initialTime, relativeSpeed, CreateStopwatchSource())
        {
        }


        public DateTimeOffset Now
        {
            get
            {
                var timezoneOffset = _baseTime.Offset;
                var realNow = _getNow();

                var passedTime = TimeSpan.FromTicks((long)((realNow - _baseTime).Ticks * _relativeSpeed));

                try
                {
                    var result = _baseTime + passedTime + _realTimeOffset;

                    return result;
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public Task WhenComes(DateTimeOffset targetTime)
        {
            var awaiting = new TaskCompletionSource<bool>();

            Subscribe(targetTime, () =>
            {
                if (Now >= targetTime)
                {
                    awaiting.TrySetResult(true);
                    return true;
                }

                return false;
            });

            return awaiting.Task;
        }

        public Task WhenPasses(TimeSpan time)
            => WhenComes(Now + time);

        void CheckCallbacks()
        {
            _onTimeChanged.ForEach(c =>
            {
                var fired = c.Callback();
                if (fired)
                {
                    _onTimeChanged.Remove(c);
                }
            });
        }

        void Subscribe(DateTimeOffset targetTime, Func<bool> callback)
        {
            _onTimeChanged.Add(new ChronosCallback(targetTime, callback));
            AttuneTimeFlow();
        }

        public void SetTime(DateTimeOffset newNow)
        {
            var newOffset = newNow - _getNow();

#if DEBUG
            var offsetsDifference = newOffset - _realTimeOffset;
#endif

            _realTimeOffset = newOffset;
            _baseTime = newNow;
            AttuneTimeFlow();
        }

        public void SetSpeed(decimal newRelativeSpeed)
        {
            _relativeSpeed = newRelativeSpeed;
            AttuneTimeFlow();
        }

        public void MoveTime(TimeSpan moveForward)
        {
            _realTimeOffset += moveForward;
            AttuneTimeFlow();
        }

        public Task WhenComes(DateTime targetTimeUtc)
            => WhenComes(new DateTimeOffset(targetTimeUtc, default(TimeSpan)));
    }
}
