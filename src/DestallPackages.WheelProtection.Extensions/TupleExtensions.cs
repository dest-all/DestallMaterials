using System;

namespace DestallMaterials.WheelProtection.Extensions.Tuples;

public static class TupleExtensions
{

    public static IEnumerator<T0> GetEnumerator<T0, T1>(this (T0, T1) tuple)
        where T1 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2>(this (T0, T1, T2) tuple)
        where T1 : T0
        where T2 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3>(this (T0, T1, T2, T3) tuple)
    where T1 : T0
    where T2 : T0
    where T3 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3, T4>(this (T0, T1, T2, T3, T4) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3, T4, T5>(this (T0, T1, T2, T3, T4, T5) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3, T4, T5, T6>(this (T0, T1, T2, T3, T4, T5, T6) tuple)
    where T1 : T0
    where T2 : T0
    where T3 : T0
    where T4 : T0
    where T5 : T0
    where T6 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3, T4, T5, T6, T7>(this (T0, T1, T2, T3, T4, T5, T6, T7) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
        yield return tuple.Item8;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this (T0, T1, T2, T3, T4, T5, T6, T7, T8) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        where T8 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
        yield return tuple.Item8;
        yield return tuple.Item9;
    }

    public static IEnumerator<T0> GetEnumerator<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this (T0, T1, T2, T3, T4, T5, T6, T7, T8, T9) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        where T8 : T0
        where T9 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
        yield return tuple.Item8;
        yield return tuple.Item9;
        yield return tuple.Item10;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1>(this (T0, T1) tuple)
        where T1 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2>(this (T0, T1, T2) tuple)
        where T1 : T0
        where T2 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3>(this (T0, T1, T2, T3) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3, T4>(this (T0, T1, T2, T3, T4) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3, T4, T5>(this (T0, T1, T2, T3, T4, T5) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3, T4, T5, T6>(this (T0, T1, T2, T3, T4, T5, T6) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3, T4, T5, T6, T7>(this (T0, T1, T2, T3, T4, T5, T6, T7) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
        yield return tuple.Item8;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this (T0, T1, T2, T3, T4, T5, T6, T7, T8) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        where T8 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
        yield return tuple.Item8;
        yield return tuple.Item9;
    }

    public static IEnumerable<T0> ToEnumerable<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this (T0, T1, T2, T3, T4, T5, T6, T7, T8, T9) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        where T8 : T0
        where T9 : T0
    {
        yield return tuple.Item1;
        yield return tuple.Item2;
        yield return tuple.Item3;
        yield return tuple.Item4;
        yield return tuple.Item5;
        yield return tuple.Item6;
        yield return tuple.Item7;
        yield return tuple.Item8;
        yield return tuple.Item9;
        yield return tuple.Item10;
    }

    public static T[] ToArray<T>(this (T, T) tuple)
        => new T[2] { tuple.Item1, tuple.Item2 };

    public static T0[] ToArray<T0, T1, T2>(this (T0, T1, T2) tuple)
        where T1 : T0
        where T2 : T0
        => new T0[3] { tuple.Item1, tuple.Item2, tuple.Item3 };

    public static T0[] ToArray<T0, T1, T2, T3>(this (T0, T1, T2, T3) tuple)
    where T1 : T0
    where T2 : T0
    where T3 : T0
    => new T0[4] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4 };

    public static T0[] ToArray<T0, T1, T2, T3, T4>(this (T0, T1, T2, T3, T4) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        => new T0[5] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5 };

    public static T0[] ToArray<T0, T1, T2, T3, T4, T5>(this (T0, T1, T2, T3, T4, T5) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        => new T0[6] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6 };

    public static T0[] ToArray<T0, T1, T2, T3, T4, T5, T6>(this (T0, T1, T2, T3, T4, T5, T6) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        => new T0[7] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7 };

    public static T0[] ToArray<T0, T1, T2, T3, T4, T5, T6, T7>(this (T0, T1, T2, T3, T4, T5, T6, T7) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        => new T0[8] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8 };

    public static T0[] ToArray<T0, T1, T2, T3, T4, T5, T6, T7, T8>(this (T0, T1, T2, T3, T4, T5, T6, T7, T8) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        where T8 : T0
        => new T0[9] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8, tuple.Item9 };

    public static T0[] ToArray<T0, T1, T2, T3, T4, T5, T6, T7, T8, T9>(this (T0, T1, T2, T3, T4, T5, T6, T7, T8, T9) tuple)
        where T1 : T0
        where T2 : T0
        where T3 : T0
        where T4 : T0
        where T5 : T0
        where T6 : T0
        where T7 : T0
        where T8 : T0
        where T9 : T0
        => new T0[10] { tuple.Item1, tuple.Item2, tuple.Item3, tuple.Item4, tuple.Item5, tuple.Item6, tuple.Item7, tuple.Item8, tuple.Item9, tuple.Item10 };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(2)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 }
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(3)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6}
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(4)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6 },
            { tuple.Item7, tuple.Item8 }
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(5)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6 },
            { tuple.Item7, tuple.Item8 },
            { tuple.Item9, tuple.Item10 }
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
    => new Dictionary<TKey, TValue>(6)
    {
        { tuple.Item1, tuple.Item2 },
        { tuple.Item3, tuple.Item4 },
        { tuple.Item5, tuple.Item6 },
        { tuple.Item7, tuple.Item8 },
        { tuple.Item9, tuple.Item10 },
        { tuple.Item11, tuple.Item12 }
    };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(7)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6 },
            { tuple.Item7, tuple.Item8 },
            { tuple.Item9, tuple.Item10 },
            { tuple.Item11, tuple.Item12 },
            { tuple.Item13, tuple.Item14 }
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(8)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6 },
            { tuple.Item7, tuple.Item8 },
            { tuple.Item9, tuple.Item10 },
            { tuple.Item11, tuple.Item12 },
            { tuple.Item13, tuple.Item14 },
            { tuple.Item15, tuple.Item16 }
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(9)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6 },
            { tuple.Item7, tuple.Item8 },
            { tuple.Item9, tuple.Item10 },
            { tuple.Item11, tuple.Item12 },
            { tuple.Item13, tuple.Item14 },
            { tuple.Item15, tuple.Item16 },
            { tuple.Item17, tuple.Item18 }
        };

    public static Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) tuple)
        => new Dictionary<TKey, TValue>(10)
        {
            { tuple.Item1, tuple.Item2 },
            { tuple.Item3, tuple.Item4 },
            { tuple.Item5, tuple.Item6 },
            { tuple.Item7, tuple.Item8 },
            { tuple.Item9, tuple.Item10 },
            { tuple.Item11, tuple.Item12 },
            { tuple.Item13, tuple.Item14 },
            { tuple.Item15, tuple.Item16 },
            { tuple.Item17, tuple.Item18 },
            { tuple.Item19, tuple.Item20 }
        };

    public static (TR, TR) Select<T, TR>(this (T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2));

    public static (TR, TR, TR) Select<T, TR>(this (T, T, T) tuple, Func<T, TR> selector)
    => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3));

    public static (TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4));

    public static (TR, TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4), selector(tuple.Item5));

    public static (TR, TR, TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4), selector(tuple.Item5), selector(tuple.Item6));

    public static (TR, TR, TR, TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4), selector(tuple.Item5), selector(tuple.Item6), selector(tuple.Item7));

    public static (TR, TR, TR, TR, TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T, T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4), selector(tuple.Item5), selector(tuple.Item6), selector(tuple.Item7), selector(tuple.Item8));

    public static (TR, TR, TR, TR, TR, TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T, T, T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4), selector(tuple.Item5), selector(tuple.Item6), selector(tuple.Item7), selector(tuple.Item8), selector(tuple.Item9));

    public static (TR, TR, TR, TR, TR, TR, TR, TR, TR, TR) Select<T, TR>(this (T, T, T, T, T, T, T, T, T, T) tuple, Func<T, TR> selector)
        => (selector(tuple.Item1), selector(tuple.Item2), selector(tuple.Item3), selector(tuple.Item4), selector(tuple.Item5), selector(tuple.Item6), selector(tuple.Item7), selector(tuple.Item8), selector(tuple.Item9), selector(tuple.Item10));

}
