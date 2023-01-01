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


    }
}
