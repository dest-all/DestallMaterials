using System;
using System.Collections.Generic;

namespace DestallMaterials.WheelProtection.Queues.QueueProcessors
{
    public struct QueueConfiguration
    {
        public QueueConfiguration(
            TimeSpan responseAwaitingDeadline,
            IEnumerable<KeyValuePair<TimeSpan, int>> actionsConstraint)
        {
            ResponseAwaitingDeadline = responseAwaitingDeadline;
            ActionsConstraint = actionsConstraint;
        }

        public IEnumerable<KeyValuePair<TimeSpan, int>> ActionsConstraint { get; }

        public TimeSpan ResponseAwaitingDeadline { get; }
    }
}
