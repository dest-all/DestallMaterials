using DestallMaterials.WheelProtection.Queues.QueueProcessors;

namespace DestallMaterials.Tests
{
    public class Tests
    {
        readonly List<string> _output = new List<string>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            var processor = new DestallMaterials.WheelProtection.Queues.QueueProcessors.CallInQueueAsynchronousProcessor<string, string>(async str => str + " " + str,
                new QueueConfiguration(TimeSpan.FromSeconds(25), new Dictionary<TimeSpan, int>
                    {
                        { TimeSpan.FromSeconds(1), 1 },
                        { TimeSpan.FromSeconds(10), 5 }
                    })
                );

            for (int i = 0; i < 40; i++)
            {
                _output.Add(await processor.RequestForExecution($"{DateTime.UtcNow}"));
            }
        }



        [Test]
        public async Task TestDeadline()
        {
            
        }
    }
}