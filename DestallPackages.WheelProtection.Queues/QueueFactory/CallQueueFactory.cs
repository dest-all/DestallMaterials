using System;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues.QueueFactory
{
    public class CallQueueFactory<TItem> : QueueFactory<TItem>
    {
        volatile bool _busy;

        readonly object _locker = new object();
        public CallQueueFactory(
            Func<TItem> createItem,
            Func<TItem, bool> toDiscard,
            int maxPoolSize,
            Action<TItem> discardAction = null,
            uint awaitingDeadlineMs = 0) : base(
                createItem,
                toDiscard,
                maxPoolSize,
                discardAction,
                awaitingDeadlineMs)
        {
        }

        public override Task<QueueItemStateManager<TItem>> OrderNewAsync()
        {
            var resultTask = base.OrderNewAsync();
            lock (_locker)
            {
                if (_busy)
                {
                    return resultTask;
                }
                _busy = true;
            }
            Task.Run(async () =>
            {
                await AnswerRequestsAsync();
                _busy = false;
            });
            return resultTask;
        }
    }
}
