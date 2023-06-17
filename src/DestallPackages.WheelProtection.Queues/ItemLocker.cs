using System;

namespace DestallMaterials.WheelProtection.Queues
{
    /// <summary>
    /// Tool that releases item for usage elsewhere on disposition.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ItemLocker<T> : IDisposable
    {
#if NET7_0_OR_GREATER
        public required T Item { get; init; }
#else
        public T Item { get; internal set; }
#endif


        public abstract void Dispose();

        public static implicit operator T(ItemLocker<T> itemLocker) => itemLocker.Item;

        public override sealed string ToString() => Item.ToString();
        public override sealed bool Equals(object obj) => Item.Equals(obj);
        public override sealed int GetHashCode() => Item.GetHashCode();
    }
}
