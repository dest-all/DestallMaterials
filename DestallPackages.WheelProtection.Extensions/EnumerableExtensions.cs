using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Linq
{
    public static class EnumerableExtensions
    {
        public static IAsyncEnumerable<TResult> SelectAsync<T, TResult>(this IEnumerable<T> items, Func<T, Task<TResult>> selector)
            => items.Select(selector).SelectResultsAsync();        

        public static async IAsyncEnumerable<T> SelectResultsAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            var tasksList = tasks.ToList();
            while (tasksList.Count > 0)
            {
                await Task.WhenAny(tasksList);
                var completedTasks = tasksList.Where(t => t.IsCompleted).ToArray();
                for (int i = 0; i < completedTasks.Length; i++)
                {
                    var completedTask = completedTasks[i];
                    tasksList.Remove(completedTask);
                    yield return completedTask.Result;
                }
            }
        }

        public static async Task<T[]> ToArrayAsync<T>(this IEnumerable<Task<T>> items)
        {
            var itemsStarted = items.ToArray();
            await Task.WhenAll(itemsStarted);
            return itemsStarted.Select(t => t.Result).ToArray();
        }

        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TIn, TKey, TValue>(this IEnumerable<TIn> ins, Func<TIn, Task<TKey>> asyncKeySelector, Func<TIn, TValue> valueSelector)
        {
            var result = new Dictionary<TKey, TValue>();

            await foreach (var item in ins.SelectAsync(async i => new
            {
                Key = await asyncKeySelector(i),
                Value = valueSelector(i)
            }))
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }

        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TIn, TKey, TValue>(this IEnumerable<TIn> ins, Func<TIn, TKey> keySelector, Func<TIn, Task<TValue>> asyncValueSelector)
        {
            var result = new Dictionary<TKey, TValue>();

            await foreach (var item in ins.SelectAsync(async i => new
            {
                Key = keySelector(i),
                Value = await asyncValueSelector(i)
            }))
            {
                result.Add(item.Key ?? throw new ArgumentNullException(), item.Value);
            }

            return result;
        }

        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TIn, TKey, TValue>(this IEnumerable<TIn> ins, Func<TIn, Task<TKey>> asyncKeySelector, Func<TIn, Task<TValue>> asyncValueSelector)
        {
            var result = new Dictionary<TKey, TValue>();

            await foreach (var item in ins.SelectAsync(async i =>
            {
                var keyTask = asyncKeySelector(i);
                var valueTask = asyncValueSelector(i);
                var result = new
                {
                    Key = await keyTask,
                    Value = await valueTask
                };
                return result;
            }))
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }

        public static bool MoveNext<T>(this IEnumerator<T> enumerator, out T current)
        {
            var result = enumerator.MoveNext();
            current = enumerator.Current;
            return result;
        }

        class InnerEnumerable<T> : IEnumerable<T>
        {
            readonly IEnumerator<T> _generalEnumerator;
            readonly int _limit;
            readonly T _firstItem;
            public InnerEnumerable(IEnumerator<T> generalEnumerator, int limit, T firstItem)
            {
                _generalEnumerator = generalEnumerator;
                _limit = limit;
                _firstItem = firstItem;
            }

            public IEnumerator<T> GetEnumerator()
            {
                int i = 0;
                if (i++ == 0)
                {
                    yield return _firstItem;
                }
                while (i++ < _limit && _generalEnumerator.MoveNext())
                {
                    yield return _generalEnumerator.Current;
                }
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> source, int chunkSize)
        {
            var generalEnumerator = source.GetEnumerator();

            while (generalEnumerator.MoveNext())
            {
                var enumerable = new InnerEnumerable<T>(generalEnumerator, chunkSize, generalEnumerator.Current);
                yield return enumerable;
            }
        }

        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
            {
                action(item);
            }
        }

        public static bool HasContent<T>(this IEnumerable<T> items) => items?.Any() == true;
    }
}
