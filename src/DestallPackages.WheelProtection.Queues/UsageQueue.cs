using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using LockerTask =
#if NETSTANDARD2_0
System.Threading.Tasks.Task<System.IDisposable>;
#else
System.Threading.Tasks.ValueTask<System.IDisposable>;
#endif

namespace DestallMaterials.WheelProtection.Queues
{
    public class UsageQueue
    {
        readonly List<object> _items = new List<object>();
        readonly List<(object, TaskCompletionSource<IDisposable>)> _requests = new List<(object, TaskCompletionSource<IDisposable>)>();

        public LockerTask OccupyAsync(object item, CancellationToken cancellationToken)
        {
            lock (_items)
            {
                var l = _items.Count;
                for (int i = 0; i < l; i++)
                {
                    var item2 = _items[i];
                    if (ReferenceEquals(item2, item))
                    {
                        TaskCompletionSource<IDisposable> request = new TaskCompletionSource<IDisposable>();
                        _requests.Add((item, request));

                        cancellationToken.Register(() =>
                        {
                            lock (_items)
                            {
                                var requestsCount = _requests.Count;
                                for (int j = 0; j < requestsCount; j++)
                                {
                                    var iRequest = _requests[j];
                                    if (ReferenceEquals(request, iRequest.Item2))
                                    {
                                        _requests.RemoveAt(j);
                                        request.SetException(new TaskCanceledException());
                                    }
                                }

                            }
                        });
#if NETSTANDARD2_0
                        return request.Task;
#else
                        return new LockerTask(request.Task);
#endif

                    }
                }

                _items.Add(item);

                var result = LockItem(item);

#if NETSTANDARD2_0
                return Task.FromResult<IDisposable>(result);
#else
                return new LockerTask(result);
#endif
            }
        }

        Locker LockItem(object item)
            => new Locker(() =>
            {
                lock (_items)
                {
                    _items.Remove(item);
                    var l = _requests.Count;
                    for (int i = 0; i < l; i++)
                    {
                        var (requestedItem, task) = _requests[i];
                        if (ReferenceEquals(requestedItem, item))
                        {
                            _requests.RemoveAt(i);
                            task.SetResult(LockItem(item));
                            return;
                        }
                    }
                }
            });

        class Locker : IDisposable
        {
            readonly Action _onDisposed;

            public Locker(Action onDisposed)
            {
                _onDisposed = onDisposed;
            }

            public void Dispose()
            {
                _onDisposed();
            }
        }

    }
}
