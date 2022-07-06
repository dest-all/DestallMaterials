using DestallMaterials.WheelProtection.Extensions.Collections;
using DestallMaterials.WheelProtection.Extensions.String;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues.QueueFactory
{
    public abstract class QueueFactory<TItem>
    {
        protected readonly Func<TItem, bool> _toDiscard;
        protected readonly Action<TItem> _discardAction;
        protected readonly int _maxPoolSize;
        protected readonly ConcurrentQueue<TaskCompletionSource<QueueItemStateManager<TItem>>> _requestsQueue = new ConcurrentQueue<TaskCompletionSource<QueueItemStateManager<TItem>>>();
        protected readonly ResourcePool<QueueItemStateManager<TItem>> _pool;
        protected readonly Func<TItem> _createItem;
        protected readonly int _awaitingDeadlineMs;

        protected volatile TaskCompletionSource<bool> AwaitingPoolChange;

        protected QueueFactory(Func<TItem> createItem,
                            Func<TItem, bool> toDiscard,
                            int maxPoolSize,
                            Action<TItem> discardAction = null,
                            uint awaitingDeadlineMs = 0)
        {
            _createItem = createItem;
            _toDiscard = toDiscard;
            _maxPoolSize = maxPoolSize;

            _pool = new ResourcePool<QueueItemStateManager<TItem>>(() =>
            {
                if (AwaitingPoolChange != null)
                {
                    AwaitingPoolChange.TrySetResult(true);
                }
            });
            _discardAction = discardAction ?? (item => { });
            _awaitingDeadlineMs = (int)awaitingDeadlineMs;
        }

        protected string TimeoutExceptionMessage
        {
            get
            {
                string poolState =
                    new
                    {
                        ItemsTotal = _pool.Count(i => i != null),
                        FreeItems = _pool.Count(i => i?.Available == true)
                    }.ToJson();
                return $"{nameof(QueueFactory<TItem>)} of {typeof(TItem).Name} has been awaited too long to give item." +
                $"\nFactory state: Pool count: {poolState}, Requests queue count: {_requestsQueue.Count}.";
            }
        }

        protected QueueItemStateManager<TItem> SeekAvailable(int startIndex,
            out int resultIndex)
        {
            for (var i = startIndex; i < _pool.Count; i++)
            {
                var item = _pool[i];
                if (item == null)
                {
                    continue;
                }
                if (_toDiscard(item.Item))
                {
                    _pool[i] = null;
                    _discardAction(item.Item);
                }
                else if (item.Available)
                {
                    resultIndex = i;
                    return item;
                }
            }
            resultIndex = _pool.Count - 1;
            return null;
        }
        protected int SeekEmptySpot()
        {
            for (int i = 0; i < _pool.Count; i++)
            {
                if (_pool[i] == null)
                {
                    return i;
                }
            }
            return -1;
        }

        async Task UntilPoolChanges()
        {
            AwaitingPoolChange = AwaitingPoolChange ?? new TaskCompletionSource<bool>();

            var waiting = AwaitingPoolChange.Task;

            await waiting;

            AwaitingPoolChange = null;
        }

        protected virtual async Task AnswerRequestsAsync()
        {
            while (_requestsQueue.Count != 0)
            {
                _pool.RemoveAll(i => i == null);
                var availableItem = SeekAvailable(0, out var reachedIndex);

                if (availableItem == null)
                {
                    if (reachedIndex == _maxPoolSize - 1)
                    {
                        await UntilPoolChanges();
                        continue;
                    }
                    availableItem = new QueueItemStateManager<TItem>(_createItem(), _pool, reachedIndex + 1);
                    _pool.Add(availableItem);
                }

                var taskToClose = _requestsQueue.WithdrawUntil(t => !t.Task.IsCompleted);

                if (taskToClose != null)
                {
                    availableItem.Available = false;
                    taskToClose.SetResult(availableItem);
                }
            }
        }

        public virtual async Task<QueueItemStateManager<TItem>> OrderNewAsync()
        {
            var taskSource = new TaskCompletionSource<QueueItemStateManager<TItem>>();

            _requestsQueue.Enqueue(taskSource);

            var task = taskSource.Task;

            if (_awaitingDeadlineMs != 0)
            {
                await Task.WhenAny(task, Task.Delay(_awaitingDeadlineMs));
                if (!task.IsCompleted)
                {
                    var exception = new TimeoutException(TimeoutExceptionMessage);
                    if (taskSource.TrySetException(exception))
                    {
                        throw exception;
                    }
                    else if (task.IsFaulted)
                    {
                        throw task.Exception;
                    }
                }
                return task.Result;
            }

            return await taskSource.Task;
        }

    }


}
