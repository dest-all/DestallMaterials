

namespace DestallMaterials.WheelProtection.Linq
{
    public static class TupleExtensions
    {


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
        }

        public static (TOut, TOut) Select<T, TOut>(this (T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)
                                        );

        public static bool Any<T>(this (T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ;

        public static bool All<T>(this (T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    ;

        public static T First<T>(this (T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(1)
            {

                                { keysValues.Item1, keysValues.Item2 },
                                        };

        public static ((T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1) tuple1,
                (T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)
                                        );

        public static (TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1) tuple1,
            (T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )
                                        );

        public static T At<T>(this (T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T) Reverse<T, TSelector>(this (T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T) Prepend<T>(this (T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2);

        public static (T, T, T) Append<T>(this (T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
            , newItem
        );

        public static T[] ToArray<T>(this (T, T) items)
            => new T[2]
            {
                        items.Item1,items.Item2
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
        }


        public static (T, T, T, T) Concat<T>(this (T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static int Sum(
            this (int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2);

        public static int Sum<T>(
            this (T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2));

        public static uint Sum(
            this (uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2);

        public static uint Sum<T>(
            this (T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2));

        public static short Sum(
            this (short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2);

        public static short Sum<T>(
            this (T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2));

        public static ushort Sum(
            this (ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2);

        public static ushort Sum<T>(
            this (T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2));

        public static long Sum(
            this (long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2);

        public static long Sum<T>(
            this (T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2));

        public static ulong Sum(
            this (ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2);

        public static ulong Sum<T>(
            this (T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2));

        public static decimal Sum(
            this (decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2);

        public static decimal Sum<T>(
            this (T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2));

        public static double Sum(
            this (double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2);

        public static double Sum<T>(
            this (T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2));

        public static float Sum(
            this (float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2);

        public static float Sum<T>(
            this (T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
        }

        public static (TOut, TOut, TOut) Select<T, TOut>(this (T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)
                                        );

        public static bool Any<T>(this (T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ;

        public static bool All<T>(this (T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    ;

        public static T First<T>(this (T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(2)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1) tuple1,
                (T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)
                                        );

        public static (TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1) tuple1,
            (T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )
                                        );

        public static T At<T>(this (T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T) Reverse<T, TSelector>(this (T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T) Prepend<T>(this (T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3);

        public static (T, T, T, T) Append<T>(this (T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T) items)
            => new T[3]
            {
                        items.Item1,items.Item2,items.Item3
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
        }


        public static (T, T, T, T, T) Concat<T>(this (T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T) Concat<T>(this (T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static int Sum(
            this (int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static int Sum<T>(
            this (T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static uint Sum(
            this (uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static uint Sum<T>(
            this (T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static short Sum(
            this (short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static short Sum<T>(
            this (T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static ushort Sum(
            this (ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static ushort Sum<T>(
            this (T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static long Sum(
            this (long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static long Sum<T>(
            this (T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static ulong Sum(
            this (ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static ulong Sum<T>(
            this (T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static decimal Sum(
            this (decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static decimal Sum<T>(
            this (T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static double Sum(
            this (double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static double Sum<T>(
            this (T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));

        public static float Sum(
            this (float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3);

        public static float Sum<T>(
            this (T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
        }

        public static (TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)
                                        );

        public static bool Any<T>(this (T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ;

        public static bool All<T>(this (T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    ;

        public static T First<T>(this (T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(3)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)
                                        );

        public static (TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )
                                        );

        public static T At<T>(this (T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T) Prepend<T>(this (T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4);

        public static (T, T, T, T, T) Append<T>(this (T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T) items)
            => new T[4]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
        }


        public static (T, T, T, T, T, T) Concat<T>(this (T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static int Sum(
            this (int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static int Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static uint Sum(
            this (uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static uint Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static short Sum(
            this (short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static short Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static ushort Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static long Sum(
            this (long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static long Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static ulong Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static decimal Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static double Sum(
            this (double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static double Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));

        public static float Sum(
            this (float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4);

        public static float Sum<T>(
            this (T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
        }

        public static (TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)
                                        );

        public static bool Any<T>(this (T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ;

        public static bool All<T>(this (T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    ;

        public static T First<T>(this (T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(4)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5);

        public static (T, T, T, T, T, T) Append<T>(this (T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T) items)
            => new T[5]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
        }


        public static (T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static int Sum(
            this (int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static int Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static uint Sum(
            this (uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static uint Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static short Sum(
            this (short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static short Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static ushort Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static long Sum(
            this (long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static long Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static ulong Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static decimal Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static double Sum(
            this (double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static double Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));

        public static float Sum(
            this (float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5);

        public static float Sum<T>(
            this (T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
            yield return items.Item6;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    ;

        public static T First<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(5)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6);

        public static (T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T) items)
            => new T[6]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
            yield return items.Item6;
        }


        public static (T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static int Sum(
            this (int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static int Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static uint Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static short Sum(
            this (short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static short Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static long Sum(
            this (long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static long Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static double Sum(
            this (double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static double Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));

        public static float Sum(
            this (float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6);

        public static float Sum<T>(
            this (T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
            yield return items.Item6;
            yield return items.Item7;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(6)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7);

        public static (T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T) items)
            => new T[7]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
            yield return items.Item6;
            yield return items.Item7;
        }


        public static (T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static int Sum(
            this (int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static short Sum(
            this (short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static long Sum(
            this (long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static double Sum(
            this (double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));

        public static float Sum(
            this (float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
            yield return items.Item6;
            yield return items.Item7;
            yield return items.Item8;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(7)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8);

        public static (T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T) items)
            => new T[8]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T) items)
        {
            yield return items.Item1;
            yield return items.Item2;
            yield return items.Item3;
            yield return items.Item4;
            yield return items.Item5;
            yield return items.Item6;
            yield return items.Item7;
            yield return items.Item8;
        }


        public static (T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static int Sum(
            this (int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static short Sum(
            this (short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static long Sum(
            this (long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static double Sum(
            this (double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));

        public static float Sum(
            this (float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T) items)
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
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(8)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9);

        public static (T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T) items)
            => new T[9]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T) items)
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
        }


        public static (T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T) items)
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
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(9)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T) items)
            => new T[10]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T) items)
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
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T) items)
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
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(10)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[11]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T) items)
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
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items)
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
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(11)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[12]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) items)
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
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(12)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[13]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13));


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

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)

                            ,

                        selector(items.Item14)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ||
                        selector(items.Item14)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    &&
                        selector(items.Item14)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item14?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }

            if (filter(items.Item14))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(13)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },

                                { keysValues.Item25, keysValues.Item26 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)

                            ,

                        (tuple1.Item14, tuple2.Item14)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )

                            ,

                        mappingFunction(
                            tuple1.Item14,
                            tuple2.Item14
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;

                case 13:
                    return tuple.Item14;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item14
                        ,

                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13
                            ,

                        input.Item14);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
                        ,

                    input.Item14
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[14]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13,items.Item14
            };

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


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)

                            ,

                        selector(items.Item14)

                            ,

                        selector(items.Item15)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ||
                        selector(items.Item14)
                    ||
                        selector(items.Item15)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    &&
                        selector(items.Item14)
                    &&
                        selector(items.Item15)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item14?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item15?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }

            if (filter(items.Item14))
            {
                result++;
            }

            if (filter(items.Item15))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(14)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },

                                { keysValues.Item25, keysValues.Item26 },

                                { keysValues.Item27, keysValues.Item28 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)

                            ,

                        (tuple1.Item14, tuple2.Item14)

                            ,

                        (tuple1.Item15, tuple2.Item15)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )

                            ,

                        mappingFunction(
                            tuple1.Item14,
                            tuple2.Item14
                        )

                            ,

                        mappingFunction(
                            tuple1.Item15,
                            tuple2.Item15
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;

                case 13:
                    return tuple.Item14;

                case 14:
                    return tuple.Item15;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item15
                        ,

                    tuple.Item14
                        ,

                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13
                            ,

                        input.Item14
                            ,

                        input.Item15);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
                        ,

                    input.Item14
                        ,

                    input.Item15
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[15]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13,items.Item14,items.Item15
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)

                            ,

                        selector(items.Item14)

                            ,

                        selector(items.Item15)

                            ,

                        selector(items.Item16)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ||
                        selector(items.Item14)
                    ||
                        selector(items.Item15)
                    ||
                        selector(items.Item16)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    &&
                        selector(items.Item14)
                    &&
                        selector(items.Item15)
                    &&
                        selector(items.Item16)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item14?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item15?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item16?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }

            if (filter(items.Item14))
            {
                result++;
            }

            if (filter(items.Item15))
            {
                result++;
            }

            if (filter(items.Item16))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(15)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },

                                { keysValues.Item25, keysValues.Item26 },

                                { keysValues.Item27, keysValues.Item28 },

                                { keysValues.Item29, keysValues.Item30 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)

                            ,

                        (tuple1.Item14, tuple2.Item14)

                            ,

                        (tuple1.Item15, tuple2.Item15)

                            ,

                        (tuple1.Item16, tuple2.Item16)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )

                            ,

                        mappingFunction(
                            tuple1.Item14,
                            tuple2.Item14
                        )

                            ,

                        mappingFunction(
                            tuple1.Item15,
                            tuple2.Item15
                        )

                            ,

                        mappingFunction(
                            tuple1.Item16,
                            tuple2.Item16
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;

                case 13:
                    return tuple.Item14;

                case 14:
                    return tuple.Item15;

                case 15:
                    return tuple.Item16;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item16
                        ,

                    tuple.Item15
                        ,

                    tuple.Item14
                        ,

                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13
                            ,

                        input.Item14
                            ,

                        input.Item15
                            ,

                        input.Item16);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
                        ,

                    input.Item14
                        ,

                    input.Item15
                        ,

                    input.Item16
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[16]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13,items.Item14,items.Item15,items.Item16
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
            yield return items.Item17;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)

                            ,

                        selector(items.Item14)

                            ,

                        selector(items.Item15)

                            ,

                        selector(items.Item16)

                            ,

                        selector(items.Item17)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ||
                        selector(items.Item14)
                    ||
                        selector(items.Item15)
                    ||
                        selector(items.Item16)
                    ||
                        selector(items.Item17)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    &&
                        selector(items.Item14)
                    &&
                        selector(items.Item15)
                    &&
                        selector(items.Item16)
                    &&
                        selector(items.Item17)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }

            if (selector(items.Item17))
            {
                return items.Item17;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }

            if (selector(items.Item17))
            {
                return items.Item17;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item14?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item15?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item16?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item17?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }

            if (filter(items.Item14))
            {
                result++;
            }

            if (filter(items.Item15))
            {
                result++;
            }

            if (filter(items.Item16))
            {
                result++;
            }

            if (filter(items.Item17))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(16)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },

                                { keysValues.Item25, keysValues.Item26 },

                                { keysValues.Item27, keysValues.Item28 },

                                { keysValues.Item29, keysValues.Item30 },

                                { keysValues.Item31, keysValues.Item32 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)

                            ,

                        (tuple1.Item14, tuple2.Item14)

                            ,

                        (tuple1.Item15, tuple2.Item15)

                            ,

                        (tuple1.Item16, tuple2.Item16)

                            ,

                        (tuple1.Item17, tuple2.Item17)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )

                            ,

                        mappingFunction(
                            tuple1.Item14,
                            tuple2.Item14
                        )

                            ,

                        mappingFunction(
                            tuple1.Item15,
                            tuple2.Item15
                        )

                            ,

                        mappingFunction(
                            tuple1.Item16,
                            tuple2.Item16
                        )

                            ,

                        mappingFunction(
                            tuple1.Item17,
                            tuple2.Item17
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;

                case 13:
                    return tuple.Item14;

                case 14:
                    return tuple.Item15;

                case 15:
                    return tuple.Item16;

                case 16:
                    return tuple.Item17;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item17
                        ,

                    tuple.Item16
                        ,

                    tuple.Item15
                        ,

                    tuple.Item14
                        ,

                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13
                            ,

                        input.Item14
                            ,

                        input.Item15
                            ,

                        input.Item16
                            ,

                        input.Item17);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
                        ,

                    input.Item14
                        ,

                    input.Item15
                        ,

                    input.Item16
                        ,

                    input.Item17
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[17]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13,items.Item14,items.Item15,items.Item16,items.Item17
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
            yield return items.Item17;
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16
                            ,

                        tuple2.Item17);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
            yield return items.Item17;
            yield return items.Item18;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)

                            ,

                        selector(items.Item14)

                            ,

                        selector(items.Item15)

                            ,

                        selector(items.Item16)

                            ,

                        selector(items.Item17)

                            ,

                        selector(items.Item18)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ||
                        selector(items.Item14)
                    ||
                        selector(items.Item15)
                    ||
                        selector(items.Item16)
                    ||
                        selector(items.Item17)
                    ||
                        selector(items.Item18)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    &&
                        selector(items.Item14)
                    &&
                        selector(items.Item15)
                    &&
                        selector(items.Item16)
                    &&
                        selector(items.Item17)
                    &&
                        selector(items.Item18)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }

            if (selector(items.Item17))
            {
                return items.Item17;
            }

            if (selector(items.Item18))
            {
                return items.Item18;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }

            if (selector(items.Item17))
            {
                return items.Item17;
            }

            if (selector(items.Item18))
            {
                return items.Item18;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item14?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item15?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item16?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item17?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item18?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }

            if (filter(items.Item14))
            {
                result++;
            }

            if (filter(items.Item15))
            {
                result++;
            }

            if (filter(items.Item16))
            {
                result++;
            }

            if (filter(items.Item17))
            {
                result++;
            }

            if (filter(items.Item18))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(17)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },

                                { keysValues.Item25, keysValues.Item26 },

                                { keysValues.Item27, keysValues.Item28 },

                                { keysValues.Item29, keysValues.Item30 },

                                { keysValues.Item31, keysValues.Item32 },

                                { keysValues.Item33, keysValues.Item34 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)

                            ,

                        (tuple1.Item14, tuple2.Item14)

                            ,

                        (tuple1.Item15, tuple2.Item15)

                            ,

                        (tuple1.Item16, tuple2.Item16)

                            ,

                        (tuple1.Item17, tuple2.Item17)

                            ,

                        (tuple1.Item18, tuple2.Item18)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )

                            ,

                        mappingFunction(
                            tuple1.Item14,
                            tuple2.Item14
                        )

                            ,

                        mappingFunction(
                            tuple1.Item15,
                            tuple2.Item15
                        )

                            ,

                        mappingFunction(
                            tuple1.Item16,
                            tuple2.Item16
                        )

                            ,

                        mappingFunction(
                            tuple1.Item17,
                            tuple2.Item17
                        )

                            ,

                        mappingFunction(
                            tuple1.Item18,
                            tuple2.Item18
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;

                case 13:
                    return tuple.Item14;

                case 14:
                    return tuple.Item15;

                case 15:
                    return tuple.Item16;

                case 16:
                    return tuple.Item17;

                case 17:
                    return tuple.Item18;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item18
                        ,

                    tuple.Item17
                        ,

                    tuple.Item16
                        ,

                    tuple.Item15
                        ,

                    tuple.Item14
                        ,

                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13
                            ,

                        input.Item14
                            ,

                        input.Item15
                            ,

                        input.Item16
                            ,

                        input.Item17
                            ,

                        input.Item18);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
                        ,

                    input.Item14
                        ,

                    input.Item15
                        ,

                    input.Item16
                        ,

                    input.Item17
                        ,

                    input.Item18
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[18]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13,items.Item14,items.Item15,items.Item16,items.Item17,items.Item18
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
            yield return items.Item17;
            yield return items.Item18;
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16
                            ,

                        tuple2.Item17);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16
                            ,

                        tuple2.Item17
                            ,

                        tuple2.Item18);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18));


        public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
            yield return items.Item17;
            yield return items.Item18;
            yield return items.Item19;
        }

        public static (TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut, TOut) Select<T, TOut>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, TOut> selector)
            => (

                        selector(items.Item1)

                            ,

                        selector(items.Item2)

                            ,

                        selector(items.Item3)

                            ,

                        selector(items.Item4)

                            ,

                        selector(items.Item5)

                            ,

                        selector(items.Item6)

                            ,

                        selector(items.Item7)

                            ,

                        selector(items.Item8)

                            ,

                        selector(items.Item9)

                            ,

                        selector(items.Item10)

                            ,

                        selector(items.Item11)

                            ,

                        selector(items.Item12)

                            ,

                        selector(items.Item13)

                            ,

                        selector(items.Item14)

                            ,

                        selector(items.Item15)

                            ,

                        selector(items.Item16)

                            ,

                        selector(items.Item17)

                            ,

                        selector(items.Item18)

                            ,

                        selector(items.Item19)
                                        );

        public static bool Any<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    ||
                        selector(items.Item2)
                    ||
                        selector(items.Item3)
                    ||
                        selector(items.Item4)
                    ||
                        selector(items.Item5)
                    ||
                        selector(items.Item6)
                    ||
                        selector(items.Item7)
                    ||
                        selector(items.Item8)
                    ||
                        selector(items.Item9)
                    ||
                        selector(items.Item10)
                    ||
                        selector(items.Item11)
                    ||
                        selector(items.Item12)
                    ||
                        selector(items.Item13)
                    ||
                        selector(items.Item14)
                    ||
                        selector(items.Item15)
                    ||
                        selector(items.Item16)
                    ||
                        selector(items.Item17)
                    ||
                        selector(items.Item18)
                    ||
                        selector(items.Item19)
                    ;

        public static bool All<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
            =>
                        selector(items.Item1)
                    &&
                        selector(items.Item2)
                    &&
                        selector(items.Item3)
                    &&
                        selector(items.Item4)
                    &&
                        selector(items.Item5)
                    &&
                        selector(items.Item6)
                    &&
                        selector(items.Item7)
                    &&
                        selector(items.Item8)
                    &&
                        selector(items.Item9)
                    &&
                        selector(items.Item10)
                    &&
                        selector(items.Item11)
                    &&
                        selector(items.Item12)
                    &&
                        selector(items.Item13)
                    &&
                        selector(items.Item14)
                    &&
                        selector(items.Item15)
                    &&
                        selector(items.Item16)
                    &&
                        selector(items.Item17)
                    &&
                        selector(items.Item18)
                    &&
                        selector(items.Item19)
                    ;

        public static T First<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }

            if (selector(items.Item17))
            {
                return items.Item17;
            }

            if (selector(items.Item18))
            {
                return items.Item18;
            }

            if (selector(items.Item19))
            {
                return items.Item19;
            }
            throw new System.InvalidOperationException("Tuple doesn't contain mathcing elements.");
        }

        public static T FirstOrDefault<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> selector)
        {

            if (selector(items.Item1))
            {
                return items.Item1;
            }

            if (selector(items.Item2))
            {
                return items.Item2;
            }

            if (selector(items.Item3))
            {
                return items.Item3;
            }

            if (selector(items.Item4))
            {
                return items.Item4;
            }

            if (selector(items.Item5))
            {
                return items.Item5;
            }

            if (selector(items.Item6))
            {
                return items.Item6;
            }

            if (selector(items.Item7))
            {
                return items.Item7;
            }

            if (selector(items.Item8))
            {
                return items.Item8;
            }

            if (selector(items.Item9))
            {
                return items.Item9;
            }

            if (selector(items.Item10))
            {
                return items.Item10;
            }

            if (selector(items.Item11))
            {
                return items.Item11;
            }

            if (selector(items.Item12))
            {
                return items.Item12;
            }

            if (selector(items.Item13))
            {
                return items.Item13;
            }

            if (selector(items.Item14))
            {
                return items.Item14;
            }

            if (selector(items.Item15))
            {
                return items.Item15;
            }

            if (selector(items.Item16))
            {
                return items.Item16;
            }

            if (selector(items.Item17))
            {
                return items.Item17;
            }

            if (selector(items.Item18))
            {
                return items.Item18;
            }

            if (selector(items.Item19))
            {
                return items.Item19;
            }
            return default;
        }

        public static bool Contains<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, T soughtItem)
        {

            if (items.Item1?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item2?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item3?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item4?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item5?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item6?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item7?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item8?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item9?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item10?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item11?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item12?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item13?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item14?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item15?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item16?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item17?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item18?.Equals(soughtItem) == true)
            {
                return true;
            }

            if (items.Item19?.Equals(soughtItem) == true)
            {
                return true;
            }
            return false;
        }

        public static int Count<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items, System.Func<T, bool> filter)
        {
            int result = 0;

            if (filter(items.Item1))
            {
                result++;
            }

            if (filter(items.Item2))
            {
                result++;
            }

            if (filter(items.Item3))
            {
                result++;
            }

            if (filter(items.Item4))
            {
                result++;
            }

            if (filter(items.Item5))
            {
                result++;
            }

            if (filter(items.Item6))
            {
                result++;
            }

            if (filter(items.Item7))
            {
                result++;
            }

            if (filter(items.Item8))
            {
                result++;
            }

            if (filter(items.Item9))
            {
                result++;
            }

            if (filter(items.Item10))
            {
                result++;
            }

            if (filter(items.Item11))
            {
                result++;
            }

            if (filter(items.Item12))
            {
                result++;
            }

            if (filter(items.Item13))
            {
                result++;
            }

            if (filter(items.Item14))
            {
                result++;
            }

            if (filter(items.Item15))
            {
                result++;
            }

            if (filter(items.Item16))
            {
                result++;
            }

            if (filter(items.Item17))
            {
                result++;
            }

            if (filter(items.Item18))
            {
                result++;
            }

            if (filter(items.Item19))
            {
                result++;
            }
            return result;
        }

        public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(this (TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue, TKey, TValue) keysValues)
            => new System.Collections.Generic.Dictionary<TKey, TValue>(18)
            {

                                { keysValues.Item1, keysValues.Item2 },

                                { keysValues.Item3, keysValues.Item4 },

                                { keysValues.Item5, keysValues.Item6 },

                                { keysValues.Item7, keysValues.Item8 },

                                { keysValues.Item9, keysValues.Item10 },

                                { keysValues.Item11, keysValues.Item12 },

                                { keysValues.Item13, keysValues.Item14 },

                                { keysValues.Item15, keysValues.Item16 },

                                { keysValues.Item17, keysValues.Item18 },

                                { keysValues.Item19, keysValues.Item20 },

                                { keysValues.Item21, keysValues.Item22 },

                                { keysValues.Item23, keysValues.Item24 },

                                { keysValues.Item25, keysValues.Item26 },

                                { keysValues.Item27, keysValues.Item28 },

                                { keysValues.Item29, keysValues.Item30 },

                                { keysValues.Item31, keysValues.Item32 },

                                { keysValues.Item33, keysValues.Item34 },

                                { keysValues.Item35, keysValues.Item36 },
                                        };

        public static ((T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2), (T1, T2)) Zip<T1, T2>(this
                (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
                (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2)
            => (

                        (tuple1.Item1, tuple2.Item1)

                            ,

                        (tuple1.Item2, tuple2.Item2)

                            ,

                        (tuple1.Item3, tuple2.Item3)

                            ,

                        (tuple1.Item4, tuple2.Item4)

                            ,

                        (tuple1.Item5, tuple2.Item5)

                            ,

                        (tuple1.Item6, tuple2.Item6)

                            ,

                        (tuple1.Item7, tuple2.Item7)

                            ,

                        (tuple1.Item8, tuple2.Item8)

                            ,

                        (tuple1.Item9, tuple2.Item9)

                            ,

                        (tuple1.Item10, tuple2.Item10)

                            ,

                        (tuple1.Item11, tuple2.Item11)

                            ,

                        (tuple1.Item12, tuple2.Item12)

                            ,

                        (tuple1.Item13, tuple2.Item13)

                            ,

                        (tuple1.Item14, tuple2.Item14)

                            ,

                        (tuple1.Item15, tuple2.Item15)

                            ,

                        (tuple1.Item16, tuple2.Item16)

                            ,

                        (tuple1.Item17, tuple2.Item17)

                            ,

                        (tuple1.Item18, tuple2.Item18)

                            ,

                        (tuple1.Item19, tuple2.Item19)
                                        );

        public static (TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult, TResult) Map<T1, T2, TResult>(this
            (T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1, T1) tuple1,
            (T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2, T2) tuple2,
            System.Func<T1, T2, TResult> mappingFunction)
            => (

                        mappingFunction(
                            tuple1.Item1,
                            tuple2.Item1
                        )

                            ,

                        mappingFunction(
                            tuple1.Item2,
                            tuple2.Item2
                        )

                            ,

                        mappingFunction(
                            tuple1.Item3,
                            tuple2.Item3
                        )

                            ,

                        mappingFunction(
                            tuple1.Item4,
                            tuple2.Item4
                        )

                            ,

                        mappingFunction(
                            tuple1.Item5,
                            tuple2.Item5
                        )

                            ,

                        mappingFunction(
                            tuple1.Item6,
                            tuple2.Item6
                        )

                            ,

                        mappingFunction(
                            tuple1.Item7,
                            tuple2.Item7
                        )

                            ,

                        mappingFunction(
                            tuple1.Item8,
                            tuple2.Item8
                        )

                            ,

                        mappingFunction(
                            tuple1.Item9,
                            tuple2.Item9
                        )

                            ,

                        mappingFunction(
                            tuple1.Item10,
                            tuple2.Item10
                        )

                            ,

                        mappingFunction(
                            tuple1.Item11,
                            tuple2.Item11
                        )

                            ,

                        mappingFunction(
                            tuple1.Item12,
                            tuple2.Item12
                        )

                            ,

                        mappingFunction(
                            tuple1.Item13,
                            tuple2.Item13
                        )

                            ,

                        mappingFunction(
                            tuple1.Item14,
                            tuple2.Item14
                        )

                            ,

                        mappingFunction(
                            tuple1.Item15,
                            tuple2.Item15
                        )

                            ,

                        mappingFunction(
                            tuple1.Item16,
                            tuple2.Item16
                        )

                            ,

                        mappingFunction(
                            tuple1.Item17,
                            tuple2.Item17
                        )

                            ,

                        mappingFunction(
                            tuple1.Item18,
                            tuple2.Item18
                        )

                            ,

                        mappingFunction(
                            tuple1.Item19,
                            tuple2.Item19
                        )
                                        );

        public static T At<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, byte index)
        {
            switch (index)
            {

                case 0:
                    return tuple.Item1;

                case 1:
                    return tuple.Item2;

                case 2:
                    return tuple.Item3;

                case 3:
                    return tuple.Item4;

                case 4:
                    return tuple.Item5;

                case 5:
                    return tuple.Item6;

                case 6:
                    return tuple.Item7;

                case 7:
                    return tuple.Item8;

                case 8:
                    return tuple.Item9;

                case 9:
                    return tuple.Item10;

                case 10:
                    return tuple.Item11;

                case 11:
                    return tuple.Item12;

                case 12:
                    return tuple.Item13;

                case 13:
                    return tuple.Item14;

                case 14:
                    return tuple.Item15;

                case 15:
                    return tuple.Item16;

                case 16:
                    return tuple.Item17;

                case 17:
                    return tuple.Item18;

                case 18:
                    return tuple.Item19;
                default:
                    throw new System.ArgumentOutOfRangeException(nameof(index));
            }
        }

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Reverse<T, TSelector>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple, System.Func<T, TSelector> selector)
            => (
                    tuple.Item19
                        ,

                    tuple.Item18
                        ,

                    tuple.Item17
                        ,

                    tuple.Item16
                        ,

                    tuple.Item15
                        ,

                    tuple.Item14
                        ,

                    tuple.Item13
                        ,

                    tuple.Item12
                        ,

                    tuple.Item11
                        ,

                    tuple.Item10
                        ,

                    tuple.Item9
                        ,

                    tuple.Item8
                        ,

                    tuple.Item7
                        ,

                    tuple.Item6
                        ,

                    tuple.Item5
                        ,

                    tuple.Item4
                        ,

                    tuple.Item3
                        ,

                    tuple.Item2
                        ,

                    tuple.Item1);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Prepend<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
            => (
                newItem,

                        input.Item1
                            ,

                        input.Item2
                            ,

                        input.Item3
                            ,

                        input.Item4
                            ,

                        input.Item5
                            ,

                        input.Item6
                            ,

                        input.Item7
                            ,

                        input.Item8
                            ,

                        input.Item9
                            ,

                        input.Item10
                            ,

                        input.Item11
                            ,

                        input.Item12
                            ,

                        input.Item13
                            ,

                        input.Item14
                            ,

                        input.Item15
                            ,

                        input.Item16
                            ,

                        input.Item17
                            ,

                        input.Item18
                            ,

                        input.Item19);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Append<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) input, T newItem)
        => (

                    input.Item1
                        ,

                    input.Item2
                        ,

                    input.Item3
                        ,

                    input.Item4
                        ,

                    input.Item5
                        ,

                    input.Item6
                        ,

                    input.Item7
                        ,

                    input.Item8
                        ,

                    input.Item9
                        ,

                    input.Item10
                        ,

                    input.Item11
                        ,

                    input.Item12
                        ,

                    input.Item13
                        ,

                    input.Item14
                        ,

                    input.Item15
                        ,

                    input.Item16
                        ,

                    input.Item17
                        ,

                    input.Item18
                        ,

                    input.Item19
            , newItem
        );

        public static T[] ToArray<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
            => new T[19]
            {
                        items.Item1,items.Item2,items.Item3,items.Item4,items.Item5,items.Item6,items.Item7,items.Item8,items.Item9,items.Item10,items.Item11,items.Item12,items.Item13,items.Item14,items.Item15,items.Item16,items.Item17,items.Item18,items.Item19
            };

        public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items)
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
            yield return items.Item15;
            yield return items.Item16;
            yield return items.Item17;
            yield return items.Item18;
            yield return items.Item19;
        }


        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16
                            ,

                        tuple2.Item17);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16
                            ,

                        tuple2.Item17
                            ,

                        tuple2.Item18);

        public static (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) Concat<T>(this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple1, (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) tuple2)
            => (

                        tuple1.Item1,

                        tuple1.Item2,

                        tuple1.Item3,

                        tuple1.Item4,

                        tuple1.Item5,

                        tuple1.Item6,

                        tuple1.Item7,

                        tuple1.Item8,

                        tuple1.Item9,

                        tuple1.Item10,

                        tuple1.Item11,

                        tuple1.Item12,

                        tuple1.Item13,

                        tuple1.Item14,

                        tuple1.Item15,

                        tuple1.Item16,

                        tuple1.Item17,

                        tuple1.Item18,

                        tuple1.Item19,

                        tuple2.Item1
                            ,

                        tuple2.Item2
                            ,

                        tuple2.Item3
                            ,

                        tuple2.Item4
                            ,

                        tuple2.Item5
                            ,

                        tuple2.Item6
                            ,

                        tuple2.Item7
                            ,

                        tuple2.Item8
                            ,

                        tuple2.Item9
                            ,

                        tuple2.Item10
                            ,

                        tuple2.Item11
                            ,

                        tuple2.Item12
                            ,

                        tuple2.Item13
                            ,

                        tuple2.Item14
                            ,

                        tuple2.Item15
                            ,

                        tuple2.Item16
                            ,

                        tuple2.Item17
                            ,

                        tuple2.Item18
                            ,

                        tuple2.Item19);

        public static int Sum(
            this (int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int, int) numbers
        ) => (int)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static int Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, int> selector
        ) => (int)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static uint Sum(
            this (uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint, uint) numbers
        ) => (uint)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static uint Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, uint> selector
        ) => (uint)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static short Sum(
            this (short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short, short) numbers
        ) => (short)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static short Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, short> selector
        ) => (short)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static ushort Sum(
            this (ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort, ushort) numbers
        ) => (ushort)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static ushort Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ushort> selector
        ) => (ushort)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static long Sum(
            this (long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long, long) numbers
        ) => (long)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static long Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, long> selector
        ) => (long)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static ulong Sum(
            this (ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong, ulong) numbers
        ) => (ulong)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static ulong Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, ulong> selector
        ) => (ulong)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static decimal Sum(
            this (decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal, decimal) numbers
        ) => (decimal)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static decimal Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, decimal> selector
        ) => (decimal)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static double Sum(
            this (double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double, double) numbers
        ) => (double)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static double Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, double> selector
        ) => (double)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));

        public static float Sum(
            this (float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float, float) numbers
        ) => (float)(numbers.Item1 + numbers.Item2 + numbers.Item3 + numbers.Item4 + numbers.Item5 + numbers.Item6 + numbers.Item7 + numbers.Item8 + numbers.Item9 + numbers.Item10 + numbers.Item11 + numbers.Item12 + numbers.Item13 + numbers.Item14 + numbers.Item15 + numbers.Item16 + numbers.Item17 + numbers.Item18 + numbers.Item19);

        public static float Sum<T>(
            this (T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T, T) items,
            System.Func<T, float> selector
        ) => (float)(selector(items.Item1) + selector(items.Item2) + selector(items.Item3) + selector(items.Item4) + selector(items.Item5) + selector(items.Item6) + selector(items.Item7) + selector(items.Item8) + selector(items.Item9) + selector(items.Item10) + selector(items.Item11) + selector(items.Item12) + selector(items.Item13) + selector(items.Item14) + selector(items.Item15) + selector(items.Item16) + selector(items.Item17) + selector(items.Item18) + selector(items.Item19));
    }

}

