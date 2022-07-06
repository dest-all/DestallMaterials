using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DestallMaterials.WheelProtection.Extensions.Collections
{
    public static class CollectionExtensions
    {
        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int sliceSize)
        {
            var result = new List<T[]>();
            int i = 0;
            var arr = new T[sliceSize];
            foreach (var item in source)
            {
                arr[i++] = item;
                if (i == sliceSize)
                {
                    result.Add(arr);
                    arr = new T[sliceSize];
                    i = 0;
                }
            }
            if (i > 0)
            {
                var last = new T[i];
                Array.ConstrainedCopy(arr, 0, last, 0, i);
                result.Add(last);
            }
            return result;
        }

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

        public static bool HasContent<T>(this IEnumerable<T> items)
        {
            return items?.Any() == true;
        }
    }
}
