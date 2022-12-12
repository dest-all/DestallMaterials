using System;
using System.Collections.Generic;
using System.Linq;

namespace DestallMaterials.WheelProtection.Queues
{
    public static class Algorithms
    {
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
