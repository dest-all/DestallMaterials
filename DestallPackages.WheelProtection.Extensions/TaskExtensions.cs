using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Threading.Tasks.Task;

namespace DestallMaterials.WheelProtection.Extensions.Tasks
{
    public static class TaskExtensions
    {
        public static Task<T> DefaultOnError<T>(this Task<T> task)
            => task.ContinueWith(t => t.IsFaulted ? default : t.Result);

        public static Task IgnoreError(this Task task)
            => task.ContinueWith(t => { });

        public static Task<T> WithinDeadline<T>(this Task<T> task, TimeSpan deadline) => WhenAny(task, Delay(deadline))
            .ContinueWith(t => !t.IsCompleted ? throw new TimeoutException($"Task is not completed within period of {deadline}.") : task.Result);

        public static Task WithinDeadline(this Task task, TimeSpan deadline)
            => WhenAny(task, Delay(deadline))
            .ContinueWith(t =>
            {
                if (!t.IsCompleted)
                {
                    throw new TimeoutException($"Task is not completed within period of {deadline}.");
                }
            });

        public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> task, Func<TIn, Task<TOut>> asyncSelector)
        {
            var res1 = await task;
            var res2 = await asyncSelector(res1);

            return res2;
        }

        public static async Task<TOut> Then<TIn, TOut>(this Task<TIn> task, Func<TIn, TOut> selector)
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

        public static async Task Then<TIn, TOut>(this Task<TIn> task, Func<TIn, Task<TIn>> asyncAction)
        {
            var res1 = await task;
            await asyncAction(res1);
        }



        public static async Task<TOut> Then<TIn, TOut>(this Task task, Func<Task<TOut>> asyncSelector)
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

        public static async Task Then<TOut>(this Task task, Action action)
        {
            await task;
            action();
        }

        public static async Task Then<TOut>(this Task task, Func<Task> asyncAction)
        {
            await task;
            await asyncAction();
        }

    }
}
