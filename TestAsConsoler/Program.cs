using DestallMaterials.WheelProtection.Queues.QueueProcessors;

var processor = new DestallMaterials.WheelProtection.Queues.QueueProcessors.CallInQueueAsynchronousProcessor<string, bool>(async str =>
{
    Console.WriteLine(str);
    return true;
},
    new QueueConfiguration(TimeSpan.FromSeconds(250), new Dictionary<TimeSpan, int>
    {
        { TimeSpan.FromSeconds(1), 1 },
        { TimeSpan.FromSeconds(10), 5 }
    })
    );

var tasks = Enumerable.Range(0, 100).Select(i => processor.RequestForExecution($"{i}.")).ToArray();

await Task.WhenAll(tasks);

var a = 55;