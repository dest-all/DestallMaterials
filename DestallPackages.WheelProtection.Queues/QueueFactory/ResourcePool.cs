using System;
using System.Collections;
using System.Collections.Generic;

namespace DestallMaterials.WheelProtection.Queues.QueueFactory
{
    public class ResourcePool<TResource> : IList<TResource>
    {
        readonly List<TResource> _resources = new List<TResource>();

        public void FireStateChanged()
        {
            OnResourceFreed();
        }

        private readonly Action OnResourceFreed;

        public ResourcePool(Action onResourceFreed)
        {
            OnResourceFreed = onResourceFreed ?? (() => { });
        }


        #region Boilerplate

        public TResource this[int index] { get => _resources[index]; set => _resources[index] = value; }

        public int Count => _resources.Count;

        public bool IsReadOnly => (_resources as ICollection<TResource>).IsReadOnly;

        public void Add(TResource item)
        {
            _resources.Add(item);
        }

        public void Clear()
        {
            _resources.Clear();
        }

        public bool Contains(TResource item)
        {
            return _resources.Contains(item);
        }

        public void CopyTo(TResource[] array, int arrayIndex)
        {
            _resources.CopyTo(array, arrayIndex);
        }

        public IEnumerator<TResource> GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        public int IndexOf(TResource item)
        {
            return _resources.IndexOf(item);
        }

        public void Insert(int index, TResource item)
        {
            _resources.Insert(index, item);
        }

        public bool Remove(TResource item)
        {
            return _resources.Remove(item);
        }

        public void RemoveAt(int index)
        {
            _resources.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _resources.GetEnumerator();
        }

        public void RemoveAll(Func<TResource, bool> condition) => _resources.RemoveAll(i => condition(i));

        #endregion
    }


}
