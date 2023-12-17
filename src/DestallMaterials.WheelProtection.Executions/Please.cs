using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Executions
{
    public static class Please
    {
        public static async Task<TResult> RepeatUntilSuccessAsync<TResult>(
            this Func<Task<TResult>> function,
            int maxTriesCount,
            TimeSpan awaitBetweenTries = default
        )
        {
            while (maxTriesCount-- > 0)
            {
                try
                {
                    return await function();
                }
                catch (Exception ex)
                {
                    if (maxTriesCount == 0)
                    {
                        throw;
                    }
                    await Task.Delay(awaitBetweenTries);
                }
            }
            return default;
        }

        public static async Task<TResult> RepeatUntilSuccessAsync<TResult>(
            this Func<Task<TResult>> function,
            int maxTriesCount,
            Func<TResult, bool> validityCriterion,
            TimeSpan awaitBetweenTries = default
        )
        {
            while (maxTriesCount-- > 0)
            {
                try
                {
                    var result = await function();
                    if (validityCriterion(result) || maxTriesCount == 0)
                    {
                        return result;
                    }
                }
                catch (Exception ex)
                {
                    if (maxTriesCount == 0)
                    {
                        throw;
                    }
                }
                await Task.Delay(awaitBetweenTries);
            }
            return default;
        }

        public static TResult MeasureExecutionTime<TResult>(
            this Func<TResult> func,
            Action<TimeSpan> doWithTimeTaken
        )
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = func();
                return result;
            }
            finally
            {
                doWithTimeTaken(stopwatch.Elapsed);
            }
        }

        public static async Task<TResult> MeasureExecutionTimeAsync<TResult>(
            this Func<Task<TResult>> func,
            Action<TimeSpan> doWithTimeTaken
        )
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = await func();
                return result;
            }
            finally
            {
                doWithTimeTaken(stopwatch.Elapsed);
            }
        }

        public static async Task MeasureExecutionTimeAsync(
            this Func<Task> func,
            Action<TimeSpan> doWithTimeTaken
        )
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                await func();
            }
            finally
            {
                doWithTimeTaken(stopwatch.Elapsed);
            }
        }

        public static async Task<T> MeasureExecutionTimeAsync<T>(this Task<T> task, Action<TimeSpan> doWithTimeTaken)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                return await task;
            }
            finally
            {
                doWithTimeTaken(stopwatch.Elapsed);
            }
        }

        public static void MeasureExecutionTime(
            this Action action,
            Action<TimeSpan> doWithTimeTaken
        )
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                action();
            }
            finally
            {
                stopwatch.Stop();
                doWithTimeTaken(stopwatch.Elapsed);
            }
        }

        public static async Task<TResult> ReturnOnErrorAsync<TResult>(
            this Func<Task<TResult>> func,
            Func<Exception, TResult> onError
        )
        {
            try
            {
                return await func();
            }
            catch (Exception ex)
            {
                return onError(ex);
            }
        }

        public static TResult ReturnOnError<TResult>(
            this Func<TResult> func,
            Func<Exception, TResult> onError
        )
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                return onError(ex);
            }
        }
    }
}
