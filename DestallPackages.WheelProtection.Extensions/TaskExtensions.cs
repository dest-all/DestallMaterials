using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Extensions.Tasks
{
    public static class TaskExtensions
    {
        /// <summary>
        /// Последовательно обрабатывает завершающиеся асинхронные задачи и ожидает завершения обработки всех задач.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="tasks"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static async Task ExecuteInQueue<TTask>(this IEnumerable<TTask> tasks, Action<TTask> action) where TTask : Task
        {
            var tasksList = tasks.ToList();
            await Task.WhenAny(tasksList);
            foreach (var task in tasksList.Where(t => t.IsCompleted).ToList())
            {
                if (task.IsCompleted)
                {
                    action(task);
                    tasksList.Remove(task);
                }
            }
            if (tasksList.Any())
            {
                await tasksList.ExecuteInQueue(action);
            }
        }

        public static async Task<T> WithinDeadline<T>(this Task<T> task, TimeSpan deadline, string message)
        {
            await Task.WhenAny(task, Task.Delay(deadline));

            if (task.IsCompleted)
            {
                return task.Result;
            }
            else
            {
                throw new TimeoutException(message);
            }
        }

        public static async Task WithinDeadline(this Task task, TimeSpan deadline, string message)
        {
            await Task.WhenAny(task, Task.Delay(deadline));

            if (!task.IsCompleted)
            {
                throw new TimeoutException(message);
            }
        }

        public static async Task WithinDeadline(this ValueTask task, TimeSpan deadline)
        {
            await Task.WhenAny(task.AsTask(), Task.Delay(deadline));

            if (!task.IsCompleted)
            {
                throw new TimeoutException();
            }
        }

        public static TResult GetResultWithDisposition<TResult>(this Task<TResult> task)
        {
            try
            {
                return task.Result;
            }
            finally
            {
                task.Dispose();
            }
        }

    }
}
