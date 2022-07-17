using DestallMaterials.WheelProtection.Linq;
using DestallMaterials.WheelProtection.Queues.QueueProcessors;

namespace DestallMaterials.Tests
{
    public class EnumerableExtensionsTests
    {
        readonly List<string> _output = new List<string>();

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task DictionaryContainsAllElementsFromIncomingEnumerable()
        {
            var random = new Random();

            var enumerable = Enumerable.Range(0, 1000).Select(async i =>
            {
                var awaitFor = (random.Next(5) + 1) * 1000;
                await Task.Delay(awaitFor);
                return awaitFor;
            });

            var result = await enumerable.ToDictionaryAsync(async at =>
            {
                var startTime = DateTime.UtcNow;
                await at;
                return (DateTime.UtcNow - startTime).TotalMilliseconds;
            }, async at => await at);

            Assert.That(result.Count, Is.EqualTo(enumerable.Count()));
        }

        [Test]
        public void TestSplit()
        {
            var result = Enumerable.Range(0, 100).Split(10).Select(e=>e.ToArray());
        }


    }
}