using System;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues.QueueProcessors
{

    public class CallInQueueAsynchronousProcessor<TPayload, TResult>
        : InQueueAsynchronousProcessor<TPayload, TResult>
    {
        private volatile bool _busy;
        readonly object _locker = new object();
        public CallInQueueAsynchronousProcessor(Func<TPayload, Task<TResult>> createItemAsync, QueueConfiguration configuration) : base(createItemAsync, configuration)
        {
        }

        protected override void OnRequestAdded(RequestForProcession request)
        {
            lock (_locker)
            {
                if (_busy)
                {
                    return;
                }
                _busy = true;
            }
            Task.Run(async () =>
            {
                await ExecuteRequestsAsync();
                _busy = false;
            });
        }
    }
}
