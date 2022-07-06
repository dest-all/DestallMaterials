using System;
using System.Collections.Generic;
using System.Linq;

namespace DestallMaterials.WheelProtection.Queues.QueueProcessors
{
    static class Algorithms
    {
        public static TimeSpan CalculateOverallAwaitTime(IReadOnlyList<KeyValuePair<TimeSpan, int>> actionConstraints, List<DateTime> executionsHistory)
        {
            var result = TimeSpan.Zero;
            if (executionsHistory.Count == 0)
            {
                return result;
            }

            var currentTime = DateTime.UtcNow;

            Span<DateTime> firstCalls = stackalloc DateTime[actionConstraints.Count];
            Span<int> actionsInLimits = stackalloc int[actionConstraints.Count];

            for (var historyPointIndex = executionsHistory.Count - 1; historyPointIndex >= 0; historyPointIndex--)
            {
                var actionDate = executionsHistory[historyPointIndex];
                for (var constraintIndex = actionConstraints.Count - 1; constraintIndex >= 0; constraintIndex--)
                {
                    var constraint = actionConstraints[constraintIndex];

                    var withinConstraint = currentTime - actionDate < constraint.Key;

                    if (withinConstraint)
                    {
                        actionsInLimits[constraintIndex]++;
                        if (actionsInLimits[constraintIndex] == constraint.Value)
                        {
                            firstCalls[constraintIndex] = actionDate;
                            break;
                        }
                    }
                }
            }

            bool needsCleaning = executionsHistory.Count > actionConstraints[0].Value * 2;
            if (needsCleaning)
            {
                executionsHistory.RemoveAll(e => actionConstraints.All(ac => DateTime.UtcNow - e > ac.Key));
            }

            var toAwait = TimeSpan.Zero;

            for (var i = 0; i < firstCalls.Length; i++)
            {
                var call = firstCalls[i];

                if (call == default)
                {
                    continue;
                }

                var timeLengthForConstraint = actionConstraints[i].Key;

                var toAwaitForThisConstraint = timeLengthForConstraint - (currentTime - call);

                if (toAwaitForThisConstraint > toAwait)
                {
                    toAwait = toAwaitForThisConstraint;
                }
            }

            return toAwait;
        }

        public static IReadOnlyList<KeyValuePair<TimeSpan, int>> OptimizeConstraints(this IEnumerable<KeyValuePair<TimeSpan, int>> constraints)
        {
            constraints = constraints.OrderByDescending(c => c.Key).ToArray();

            var result = constraints.Where(constraint =>
            {
                var shorterConstraints = constraints.Where(c => c.Key < constraint.Key);

                var limitIsContainedWithin = shorterConstraints.FirstOrDefault(sc => Math.Ceiling(constraint.Key.Ticks / (double)sc.Key.Ticks) * sc.Value < constraint.Value);

                return limitIsContainedWithin.Value == default;
            }).ToList();

            return result;
        }

    }
}
