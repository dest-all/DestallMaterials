using System;

namespace DestallMaterials.WheelProtection.Queues.QueueFactory
{
    public class QueueItemStateManager<TItem> : IDisposable
    {
        ResourcePool<QueueItemStateManager<TItem>> _pool;
        readonly int _poolPosition;

        internal bool Available { get; set; }

        public QueueItemStateManager(
            TItem item,
            ResourcePool<QueueItemStateManager<TItem>> pool,
            int poolPosition
            )
        {
            Item = item;
            _pool = pool;
            _poolPosition = poolPosition;
        }

        public TItem Item { get; private set; }

        void IDisposable.Dispose()
        {
            Discard();
        }

        public void Discard()
        {
            _pool[_poolPosition] = null;
            _pool = null;
            Available = false;
            _pool.FireStateChanged();
        }

        public void ReturnToPool()
        {
            Available = true;
            _pool.FireStateChanged();
        }

    }


}
