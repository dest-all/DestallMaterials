namespace EverModern.Extensions.Tuples
{
	public static class TupleExtensions
{
		[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T) items)
            {
                yield return items.Item1;
yield return items.Item2;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T[] ToArray<T>(this (T, T) items)
                => System.Linq.Enumerable.ToArray(items.AsEnumerable());
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static (TOut, TOut) Select<T, TOut>(this (T, T) items, System.Func<T, TOut> selector)
                    => (selector(items.Item1), selector(items.Item2));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static bool Any<T>(this (T, T) items, System.Func<T, bool> selector)
                    => (selector(items.Item1) || selector(items.Item2));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static bool All<T>(this (T, T) items, System.Func<T, bool> selector)
                    => (selector(items.Item1) && selector(items.Item2));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T First<T>(this (T, T) items, System.Func<T, bool> selector)
            {
                if (selector(items.Item1)) { return items.Item1; }
if (selector(items.Item2)) { return items.Item2; }
                throw new System.InvalidOperationException("No elements in the multitude match the predicate.");
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T FirstOrDefault<T>(this (T, T) items, System.Func<T, bool> selector)
            {
                if (selector(items.Item1)) { return items.Item1; }
if (selector(items.Item2)) { return items.Item2; }
                return default;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static int Count<T>(this (T, T) items, System.Func<T, bool> selector)
            {
                int result = 0;
                if (selector(items.Item1)) { result++; }
if (selector(items.Item2)) { result++; }
                return result;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T) items)
            {
                yield return items.Item1;
yield return items.Item2;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
                    this (TKey, TValue) items)
                => new System.Collections.Generic.Dictionary<TKey, TValue>(1)
                {
                    [items.Item1] = items.Item2
                };
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            {
                yield return items.Item1;
yield return items.Item2;
yield return items.Item3;
yield return items.Item4;
yield return items.Item5;
yield return items.Item6;
yield return items.Item7;
yield return items.Item8;
yield return items.Item9;
yield return items.Item10;
yield return items.Item11;
yield return items.Item12;
yield return items.Item13;
yield return items.Item14;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
                => System.Linq.Enumerable.ToArray(items.AsEnumerable());
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
                    => (selector(items.Item1), selector(items.Item2), selector(items.Item3), selector(items.Item4), selector(items.Item5), selector(items.Item6), selector(items.Item7), selector(items.Item8), selector(items.Item9), selector(items.Item10), selector(items.Item11), selector(items.Item12), selector(items.Item13), selector(items.Item14));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
                    => (selector(items.Item1) || selector(items.Item2) || selector(items.Item3) || selector(items.Item4) || selector(items.Item5) || selector(items.Item6) || selector(items.Item7) || selector(items.Item8) || selector(items.Item9) || selector(items.Item10) || selector(items.Item11) || selector(items.Item12) || selector(items.Item13) || selector(items.Item14));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
                    => (selector(items.Item1) && selector(items.Item2) && selector(items.Item3) && selector(items.Item4) && selector(items.Item5) && selector(items.Item6) && selector(items.Item7) && selector(items.Item8) && selector(items.Item9) && selector(items.Item10) && selector(items.Item11) && selector(items.Item12) && selector(items.Item13) && selector(items.Item14));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            {
                if (selector(items.Item1)) { return items.Item1; }
if (selector(items.Item2)) { return items.Item2; }
if (selector(items.Item3)) { return items.Item3; }
if (selector(items.Item4)) { return items.Item4; }
if (selector(items.Item5)) { return items.Item5; }
if (selector(items.Item6)) { return items.Item6; }
if (selector(items.Item7)) { return items.Item7; }
if (selector(items.Item8)) { return items.Item8; }
if (selector(items.Item9)) { return items.Item9; }
if (selector(items.Item10)) { return items.Item10; }
if (selector(items.Item11)) { return items.Item11; }
if (selector(items.Item12)) { return items.Item12; }
if (selector(items.Item13)) { return items.Item13; }
if (selector(items.Item14)) { return items.Item14; }
                throw new System.InvalidOperationException("No elements in the multitude match the predicate.");
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            {
                if (selector(items.Item1)) { return items.Item1; }
if (selector(items.Item2)) { return items.Item2; }
if (selector(items.Item3)) { return items.Item3; }
if (selector(items.Item4)) { return items.Item4; }
if (selector(items.Item5)) { return items.Item5; }
if (selector(items.Item6)) { return items.Item6; }
if (selector(items.Item7)) { return items.Item7; }
if (selector(items.Item8)) { return items.Item8; }
if (selector(items.Item9)) { return items.Item9; }
if (selector(items.Item10)) { return items.Item10; }
if (selector(items.Item11)) { return items.Item11; }
if (selector(items.Item12)) { return items.Item12; }
if (selector(items.Item13)) { return items.Item13; }
if (selector(items.Item14)) { return items.Item14; }
                return default;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            {
                int result = 0;
                if (selector(items.Item1)) { result++; }
if (selector(items.Item2)) { result++; }
if (selector(items.Item3)) { result++; }
if (selector(items.Item4)) { result++; }
if (selector(items.Item5)) { result++; }
if (selector(items.Item6)) { result++; }
if (selector(items.Item7)) { result++; }
if (selector(items.Item8)) { result++; }
if (selector(items.Item9)) { result++; }
if (selector(items.Item10)) { result++; }
if (selector(items.Item11)) { result++; }
if (selector(items.Item12)) { result++; }
if (selector(items.Item13)) { result++; }
if (selector(items.Item14)) { result++; }
                return result;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            {
                yield return items.Item1;
yield return items.Item2;
yield return items.Item3;
yield return items.Item4;
yield return items.Item5;
yield return items.Item6;
yield return items.Item7;
yield return items.Item8;
yield return items.Item9;
yield return items.Item10;
yield return items.Item11;
yield return items.Item12;
yield return items.Item13;
yield return items.Item14;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
                    this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) items)
                => new System.Collections.Generic.Dictionary<TKey, TValue>(7)
                {
                    [items.Item1] = items.Item2,
[items.Item3] = items.Item4,
[items.Item5] = items.Item6,
[items.Item7] = items.Item8,
[items.Item9] = items.Item10,
[items.Item11] = items.Item12,
[items.Item13] = items.Item14
                };
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T) items)
            {
                yield return items.Item1;
yield return items.Item2;
yield return items.Item3;
yield return items.Item4;
yield return items.Item5;
yield return items.Item6;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T[] ToArray<T>(this (T, T, T, T, T, T) items)
                => System.Linq.Enumerable.ToArray(items.AsEnumerable());
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static (TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T) items, System.Func<T, TOut> selector)
                    => (selector(items.Item1), selector(items.Item2), selector(items.Item3), selector(items.Item4), selector(items.Item5), selector(items.Item6));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static bool Any<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
                    => (selector(items.Item1) || selector(items.Item2) || selector(items.Item3) || selector(items.Item4) || selector(items.Item5) || selector(items.Item6));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static bool All<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
                    => (selector(items.Item1) && selector(items.Item2) && selector(items.Item3) && selector(items.Item4) && selector(items.Item5) && selector(items.Item6));
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T First<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
            {
                if (selector(items.Item1)) { return items.Item1; }
if (selector(items.Item2)) { return items.Item2; }
if (selector(items.Item3)) { return items.Item3; }
if (selector(items.Item4)) { return items.Item4; }
if (selector(items.Item5)) { return items.Item5; }
if (selector(items.Item6)) { return items.Item6; }
                throw new System.InvalidOperationException("No elements in the multitude match the predicate.");
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static T FirstOrDefault<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
            {
                if (selector(items.Item1)) { return items.Item1; }
if (selector(items.Item2)) { return items.Item2; }
if (selector(items.Item3)) { return items.Item3; }
if (selector(items.Item4)) { return items.Item4; }
if (selector(items.Item5)) { return items.Item5; }
if (selector(items.Item6)) { return items.Item6; }
                return default;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static int Count<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
            {
                int result = 0;
                if (selector(items.Item1)) { result++; }
if (selector(items.Item2)) { result++; }
if (selector(items.Item3)) { result++; }
if (selector(items.Item4)) { result++; }
if (selector(items.Item5)) { result++; }
if (selector(items.Item6)) { result++; }
                return result;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T) items)
            {
                yield return items.Item1;
yield return items.Item2;
yield return items.Item3;
yield return items.Item4;
yield return items.Item5;
yield return items.Item6;
            }
[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]
    public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
                    this (TKey, TValue, TKey, TValue, TKey, TValue) items)
                => new System.Collections.Generic.Dictionary<TKey, TValue>(3)
                {
                    [items.Item1] = items.Item2,
[items.Item3] = items.Item4,
[items.Item5] = items.Item6
                };
	}
}
