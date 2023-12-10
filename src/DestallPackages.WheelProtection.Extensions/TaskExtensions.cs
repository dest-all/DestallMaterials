using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Threading.Tasks.Task;

namespace DestallMaterials.WheelProtection.Extensions.Tasks;

public static class TaskExtensions
{
    public static Task<T> DefaultOnError<T>(this Task<T> task) =>
        task.ContinueWith(t => t.IsFaulted ? default : t.Result);

    public static Task IgnoreError(this Task task) => task.ContinueWith(t => { });

    public static Task<T> WithinDeadline<T>(this Task<T> task, TimeSpan deadline) =>
        WhenAny(task, Delay(deadline))
            .ContinueWith(
                t =>
                    !t.IsCompleted
                        ? throw new TimeoutException(
                            $"Task is not completed within period of {deadline}."
                        )
                        : task.Result
            );

    public static Task WithinDeadline(this Task task, TimeSpan deadline) =>
        WhenAny(task, Delay(deadline))
            .ContinueWith(t =>
            {
                if (!t.IsCompleted)
                {
                    throw new TimeoutException(
                        $"Task is not completed within period of {deadline}."
                    );
                }
            });

    public static async Task<TOut> Then<TIn, TOut>(
        this Task<TIn> task,
        Func<TIn, Task<TOut>> asyncSelector
    )
    {
        var res1 = await task;
        var res2 = await asyncSelector(res1);

        return res2;
    }

    public static async Task<TOut> Then<TIn, TOut>(
        this Task<TIn> task,
        Func<TIn, TOut> selector
    )
    {
        var res1 = await task;
        var res2 = selector(res1);

        return res2;
    }

    public static async Task Then<TIn>(this Task<TIn> task, Action<TIn> selector)
    {
        var res1 = await task;
        selector(res1);
    }

    public static async Task Then<TIn, TOut>(this Task<TIn> task, Action<TIn> action)
    {
        var res1 = await task;
        action(res1);
    }

    public static async Task Then<TIn>(
        this Task<TIn> task,
        Func<TIn, Task> asyncAction
    )
    {
        var res1 = await task;
        await asyncAction(res1);
    }

    public static async Task<TOut> Then<TIn, TOut>(
        this Task task,
        Func<Task<TOut>> asyncSelector
    )
    {
        await task;
        var res2 = await asyncSelector();

        return res2;
    }

    public static async Task<TOut> Then<TOut>(this Task task, Func<TOut> selector)
    {
        await task;
        var res2 = selector();

        return res2;
    }

    public static async Task Then(this Task task, Action action)
    {
        await task;
        action();
    }

    public static async Task Then(this Task task, Func<Task> asyncAction)
    {
        await task;
        await asyncAction();
    }

    public static void Forget(this Task _) { }

    public static TaskAwaiter<ValueTuple<T1, T2>> GetAwaiter<T1, T2>(
        this ValueTuple<Task<T1>, Task<T2>> tasksTuple
    ) =>
        tasksTuple.Item1
            .Then(r1 => tasksTuple.Item2.Then(r2 => new ValueTuple<T1, T2>(r1, r2)))
            .GetAwaiter();

    public static TaskAwaiter<ValueTuple<T1, T2, T3>> GetAwaiter<T1, T2, T3>(
        this ValueTuple<Task<T1>, Task<T2>, Task<T3>> tasksTuple
    ) =>
        tasksTuple.Item1
            .Then(
                r1 =>
                    tasksTuple.Item2.Then(
                        r2 =>
                            tasksTuple.Item3.Then(r3 => new ValueTuple<T1, T2, T3>(r1, r2, r3))
                    )
            )
            .GetAwaiter();

    public static TaskAwaiter<ValueTuple<T1, T2, T3, T4>> GetAwaiter<T1, T2, T3, T4>(
        this ValueTuple<Task<T1>, Task<T2>, Task<T3>, Task<T4>> tasksTuple
    ) =>
        tasksTuple.Item1
            .Then(
                r1 =>
                    tasksTuple.Item2.Then(
                        r2 =>
                            tasksTuple.Item3.Then(
                                r3 =>
                                    tasksTuple.Item4.Then(
                                        r4 => new ValueTuple<T1, T2, T3, T4>(r1, r2, r3, r4)
                                    )
                            )
                    )
            )
            .GetAwaiter();

    public static TaskAwaiter<ValueTuple<T1, T2, T3, T4, T5>> GetAwaiter<T1, T2, T3, T4, T5>(
        this ValueTuple<Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>> tasksTuple
    ) =>
        tasksTuple.Item1
            .Then(
                r1 =>
                    tasksTuple.Item2.Then(
                        r2 =>
                            tasksTuple.Item3.Then(
                                r3 =>
                                    tasksTuple.Item4.Then(
                                        r4 =>
                                            tasksTuple.Item5.Then(
                                                r5 =>
                                                    new ValueTuple<T1, T2, T3, T4, T5>(
                                                        r1,
                                                        r2,
                                                        r3,
                                                        r4,
                                                        r5
                                                    )
                                            )
                                    )
                            )
                    )
            )
            .GetAwaiter();

    public static TaskAwaiter<ValueTuple<T1, T2, T3, T4, T5, T6>> GetAwaiter<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6
    >(this ValueTuple<Task<T1>, Task<T2>, Task<T3>, Task<T4>, Task<T5>, Task<T6>> tasksTuple) =>
        tasksTuple.Item1
            .Then(
                r1 =>
                    tasksTuple.Item2.Then(
                        r2 =>
                            tasksTuple.Item3.Then(
                                r3 =>
                                    tasksTuple.Item4.Then(
                                        r4 =>
                                            tasksTuple.Item5.Then(
                                                r5 =>
                                                    tasksTuple.Item6.Then(
                                                        r6 =>
                                                            new ValueTuple<
                                                                T1,
                                                                T2,
                                                                T3,
                                                                T4,
                                                                T5,
                                                                T6
                                                            >(r1, r2, r3, r4, r5, r6)
                                                    )
                                            )
                                    )
                            )
                    )
            )
            .GetAwaiter();

    public static TaskAwaiter<ValueTuple<T1, T2, T3, T4, T5, T6, T7>> GetAwaiter<
        T1,
        T2,
        T3,
        T4,
        T5,
        T6,
        T7
    >(
        this ValueTuple<
            Task<T1>,
            Task<T2>,
            Task<T3>,
            Task<T4>,
            Task<T5>,
            Task<T6>,
            Task<T7>
        > tasksTuple
    ) =>
        tasksTuple.Item1
            .Then(
                r1 =>
                    tasksTuple.Item2.Then(
                        r2 =>
                            tasksTuple.Item3.Then(
                                r3 =>
                                    tasksTuple.Item4.Then(
                                        r4 =>
                                            tasksTuple.Item5.Then(
                                                r5 =>
                                                    tasksTuple.Item6.Then(
                                                        r6 =>
                                                            tasksTuple.Item7.Then(
                                                                r7 =>
                                                                    new ValueTuple<
                                                                        T1,
                                                                        T2,
                                                                        T3,
                                                                        T4,
                                                                        T5,
                                                                        T6,
                                                                        T7
                                                                    >(
                                                                        r1,
                                                                        r2,
                                                                        r3,
                                                                        r4,
                                                                        r5,
                                                                        r6,
                                                                        r7
                                                                    )
                                                            )
                                                    )
                                            )
                                    )
                            )
                    )
            )
            .GetAwaiter();
}
