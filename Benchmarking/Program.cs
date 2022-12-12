using BenchmarkDotNet.Attributes;
using DestallMaterials.WheelProtection.Queues.QueueFactory;
using DestallMaterials.WheelProtection.Queues.QueueProcessors;

public class QueuesBenchmarking
{


    static readonly CallInQueueAsynchronousProcessor<string, DateTime> _queue = new CallInQueueAsynchronousProcessor<string, DateTime>();

    [Benchmark]

}