using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DestallMaterials.WheelProtection.Extensions.Collections
{
    public static class CollectionExtensions
    {
        public static T WithdrawUntil<T>(this ConcurrentQueue<T> queue, Func<T, bool> condition)
        {
            while (queue.TryDequeue(out var item))
            {
                if (condition(item))
                {
                    return item;
                }
            }
            return default;
        }

        public static bool AddIf<TItem, TCollection>(this TCollection source, TItem item, bool condition)
            where TCollection : ICollection<TItem>
        {
            if (condition)
            {
                source.Add(item);
                return true;
            }
            return false;
        }

        public static bool AddIfNot<TItem, TCollection>(this TCollection source, TItem item, bool condition)
            where TCollection : ICollection<TItem> => source.AddIfNot(item, !condition);
    }
}
