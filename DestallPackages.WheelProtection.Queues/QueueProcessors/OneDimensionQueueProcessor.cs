using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.Queues.QueueProcessors
{

    public class OneDimensionQueueProcessor<TPayload, TResult> : CallInQueueAsynchronousProcessor<TPayload, TResult>
    {
        public OneDimensionQueueProcessor(Func<TPayload, Task<TResult>> mainActionAsync, int allowedActionsNumber, TimeSpan inPeriod, TimeSpan responseAwaitingDeadline) : base(mainActionAsync,
            new QueueConfiguration(responseAwaitingDeadline, new Dictionary<TimeSpan, int>
            {
                { inPeriod, allowedActionsNumber }
            }))
        {
        }
    }
}
