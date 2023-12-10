using DestallMaterials.WheelProtection.Extensions.Tasks;
using System.Collections;

namespace DestallMaterials.WheelProtection.Extensions.Enumerables
{
    public static class EnumerableExtensions
    {
        public static IAsyncEnumerable<TResult> SelectAsync<T, TResult>(
            this IEnumerable<T> items,
            Func<T, Task<TResult>> selector
        ) => items.Select(selector).SelectResultsAsync();

        public static async IAsyncEnumerable<T> SelectResultsAsync<T>(
            this IEnumerable<Task<T>> tasks
        )
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

        public static Task<TResult[]> ToArrayAsync<T, TResult>(
            this IEnumerable<T> items,
            Func<T, Task<TResult>> selector
        ) => Task.WhenAll(items.Select(i => selector(i)));

        public static Task<T[]> ToArrayAsync<T>(this IEnumerable<Task<T>> items) =>
            Task.WhenAll(items);

        public static async Task<List<T>> ToListAsync<T>(this IEnumerable<Task<T>> items) =>
            new List<T>(await Task.WhenAll(items));

        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TIn, TKey, TValue>(
            this IEnumerable<TIn> ins,
            Func<TIn, Task<TKey>> asyncKeySelector,
            Func<TIn, TValue> valueSelector
        )
        {
            var result = new Dictionary<TKey, TValue>();

            await foreach (
                var item in ins.SelectAsync(
                    async i => new { Key = await asyncKeySelector(i), Value = valueSelector(i) }
                )
            )
            {
                result.Add(item.Key, item.Value);
            }

            return result;
        }

        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TIn, TKey, TValue>(
            this IEnumerable<TIn> ins,
            Func<TIn, TKey> keySelector,
            Func<TIn, Task<TValue>> asyncValueSelector
        )
        {
            var result = new Dictionary<TKey, TValue>();

            await foreach (
                var item in ins.SelectAsync(
                    async i => new { Key = keySelector(i), Value = await asyncValueSelector(i) }
                )
            )
            {
                result.Add(item.Key ?? throw new ArgumentNullException(), item.Value);
            }

            return result;
        }

        public static async Task<Dictionary<TKey, TValue>> ToDictionaryAsync<TIn, TKey, TValue>(
            this IEnumerable<TIn> ins,
            Func<TIn, Task<TKey>> asyncKeySelector,
            Func<TIn, Task<TValue>> asyncValueSelector
        )
        {
            var result = new Dictionary<TKey, TValue>();

            await foreach (
                var item in ins.SelectAsync(async i =>
                {
                    var keyTask = asyncKeySelector(i);
                    var valueTask = asyncValueSelector(i);
                    var result = new { Key = await keyTask, Value = await valueTask };
                    return result;
                })
            )
            {
                result.Add(item.Key, item.Value);
            }

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

        public static IEnumerable<IEnumerable<T>> Split<T>(
            this IEnumerable<T> source,
            int chunkSize
        )
        {
            var generalEnumerator = source.GetEnumerator();

            while (generalEnumerator.MoveNext())
            {
                var enumerable = new InnerEnumerable<T>(
                    generalEnumerator,
                    chunkSize,
                    generalEnumerator.Current
                );
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

        public static bool IsEmpty<T>(this IEnumerable<T>? items) => !items.HasContent();

        public static IEnumerable<T> WhereNot<T>(
            this IEnumerable<T> items,
            Func<T, bool> condition
        ) => items.Where(i => !condition(i));

        public static IEnumerable<T> DistinctBy<T>(
            this IEnumerable<T> source,
            Func<T, T, bool> areSame
        )
        {
            List<T> items = new List<T>();
            foreach (var item in source)
            {
                if (!items.Any(i => areSame(i, item)))
                {
                    items.Add(item);
                    yield return item;
                }
            }
        }

        public static bool IsOneOf<T>(this T item, IEnumerable<T> items) => items.Contains(item);

        public static bool IsOneOf<T>(this T item, params T[] items) => items.Contains(item);

        public static IEnumerable<T> Union<T>(this IEnumerable<T> items, T another)
        {
            bool met = false;
            int hash = another.GetHashCode();
            foreach (var item in items)
            {
                met = met || item.GetHashCode() == hash;
                yield return item;
            }
            if (!met)
            {
                yield return another;
            }
        }

        public static IEnumerable<T> Prepend<T>(this IEnumerable<T> items, T itemToPrepend)
        {
            yield return itemToPrepend;
            foreach (var item in items)
            {
                yield return item;
            }
        }

        public static IEnumerable<T> Append<T>(this IEnumerable<T> items, T itemToAppend)
        {
            foreach (var item in items)
            {
                yield return item;
            }
            yield return itemToAppend;
        }

        public static Task<IOrderedEnumerable<T>> OrderByAsync<T, TSelector>(
            this IEnumerable<Task<T>> source,
            Func<T, TSelector> selector
        ) => Task.WhenAll(source).Then(items => items.OrderBy(selector));

        public static Task<IOrderedEnumerable<T>> OrderByDescendingAsync<T, TSelector>(
            this IEnumerable<Task<T>> source,
            Func<T, TSelector> selector
        ) => Task.WhenAll(source).Then(items => items.OrderByDescending(selector));

        public static IReadOnlyList<T> EnsureMaterialized<T>(this IEnumerable<T> source) =>
            source as IReadOnlyList<T> ?? source.ToArray();

        public static int MetAt<T>(this IEnumerable<T> items, Func<T, bool> stopWhen)
        {
            int i = 0;
            foreach (var item in items)
            {
                if (stopWhen(item))
                {
                    return i;
                }
                i++;
            }
            return -1;
        }

        public static bool None<T>(this IEnumerable<T> source, Func<T, bool> condition)
            => !source.Any(condition);

        public static bool None<T>(this IEnumerable<T> source)
            => !source.Any();
    }
}
