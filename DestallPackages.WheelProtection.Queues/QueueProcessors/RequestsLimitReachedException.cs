using System;

namespace DestallMaterials.WheelProtection.Queues.QueueProcessors
{
    public class RequestsLimitReachedException : InvalidOperationException
    {
        public RequestsLimitReachedException(string message = "Reached limit of executed requests.") : base(message)
        {
        }
    }
}
