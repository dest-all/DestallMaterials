using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues
{

#if NETSTANDARD2_0
    internal static class StandardQueueExtensions
    {
        public static bool TryDequeue<T>(this System.Collections.Generic.Queue<T> queue, out T item)
        {
            if (queue.Count > 0)
            {
                item = queue.Dequeue();
                return true;
            }
            item = default;
            return false;
        }
    }
#endif

    public abstract class Recycler<T>
    {
        /// <summary>
        /// Construct new produced item directly.
        /// </summary>
        /// <returns></returns>
        protected abstract T CreateNew();

        /// <summary>
        /// Determine if item can be returned to pool with its instant state.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        protected abstract bool IsWell(T item);

        /// <summary>
        /// How to dispose of item, if its state is invalid, i.e. IsWell is false.
        /// </summary>
        /// <param name="item"></param>
        protected abstract void Discard(T item);

        readonly object _locker = new object();
        readonly ItemManager[] _pool;

        protected Recycler(int maxPoolSize)
        {
            _pool = new ItemManager[maxPoolSize];
        }

        readonly System.Collections.Generic.Queue<TaskCompletionSource<ItemLocker<T>>> _subscriptions = new System.Collections.Generic.Queue<TaskCompletionSource<ItemLocker<T>>>();

        /// <summary>
        /// Request new item locker from the Recycler, waiting for once it's:
        /// 1) created anew
        /// 2) released from usage
        /// 3) just taken out free from pool
        /// </summary>
        /// <returns>Tool releasing item for usage in another request.</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public
#if NETSTANDARD2_0
Task<ItemLocker<T>>
#else
ValueTask<ItemLocker<T>>
#endif

            AwaitAnother(CancellationToken cancellationToken = default)
        {
            lock (_locker)
            {
                var spanPool = _pool
#if NETSTANDARD2_0
#else
                    .AsSpan()
#endif
                    ;
                int i = 0;
                int nonEmptyCount = 0;
                for (; i < spanPool.Length; i++)
                {
                    var stateItem = spanPool[i];
                    if (stateItem is null)
                    {
                        continue;
                    }
                    nonEmptyCount++;
                    if (stateItem.Available == true)
                    {
                        stateItem.Available = false;
#if NETSTANDARD2_0
                        return Task.FromResult<ItemLocker<T>>(stateItem);
#else
                        return new ValueTask<ItemLocker<T>>(stateItem);
#endif
                    }
                }
                if (nonEmptyCount == _pool.Length)
                {
                    var taskSource = new TaskCompletionSource<ItemLocker<T>>();
                    _subscriptions.Enqueue(taskSource);
                    cancellationToken.Register(() => taskSource.TrySetCanceled());
#if NETSTANDARD2_0
                    return taskSource.Task;
#else
                    return new ValueTask<ItemLocker<T>>(taskSource.Task);
#endif
                }
                for (i = 0; i < spanPool.Length; i++)
                {
                    if (spanPool[i] is null)
                    {
                        ItemManager itemManager = CreateNewManager(i);
#if NETSTANDARD2_0
                        return Task.FromResult<ItemLocker<T>>(itemManager);
#else
                        return new ValueTask<ItemLocker<T>>(itemManager);
#endif
                    }
                }
                throw new InvalidOperationException();
            }
        }

        ItemManager CreateNewManager(int i)
        {
            var item = CreateNew();
            var itemManager = new ItemManager(OnItemReleased(i, item))
            {
                Item = item
            };
            _pool[i] = itemManager;
            return itemManager;
        }

        Action<ItemManager> OnItemReleased(int i, T item)
            => im =>
            {
                lock (_locker)
                {
                    if (IsWell(item))
                    {
                        while (_subscriptions.TryDequeue(out var request))
                        {
                            if (request.Task.IsCompleted)
                            {
                                continue;
                            }
                            if (request.TrySetResult(im))
                            {
                                return;
                            }
                        }
                        im.Available = true;
                    }
                    else
                    {
                        Discard(item);
                        im = null;
                        while (_subscriptions.TryDequeue(out var request))
                        {
                            if (request.Task.IsCompleted)
                            {
                                continue;
                            }
                            im = CreateNewManager(i);
                            if (request.TrySetResult(im))
                            {
                                return;
                            }
                        }
                        if (im is null)
                        {
                            _pool[i] = null;
                        }
                    }
                }
            };

        sealed class ItemManager : ItemLocker<T>
        {
            public bool Available { get; set; }

            readonly Action _dispose;
            public ItemManager(Action dispose)
            {
                _dispose = dispose;
            }

            public ItemManager(Action<ItemManager> dispose)
            {
                _dispose = () => dispose(this);
            }

            public override void Dispose() => _dispose();
        }
    }
}
