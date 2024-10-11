using DestallMaterials.WheelProtection.Queues;

namespace DestallMaterials.Tests;

public class QueusTesting
{
    ServerEmulator _serverEmulator = new ServerEmulator(10);

    [Test]
    public async Task Simple()
    {
        var rand = new Random(10);

        await Task.WhenAll(Enumerable.Range(0, 10).Select(i => Task.Run(async () =>
        {
            var payload = TimeSpan.FromMilliseconds(rand.Next(100) * 10);
            var response = await _serverEmulator.ProcessRequestAsync(payload);
            Console.WriteLine($"{DateTime.Now} ===> Request processed with response {response}.");
        })).ToArray());
    }

    class TestedRecycler : Recycler<object>
    {
        int _itemNumber;
        public TestedRecycler(int maxPoolSize) : base(maxPoolSize)
        {
        }

        protected override void Discard(object item)
        {
        }

        protected override bool IsWell(object item)
            => true;

        protected override bool TryCreateNew(out object item)
        {
            item = _itemNumber++;
            return true;
        }
    }

    [Test]
    public async Task Recycler()
    {
        var testedRecycler = new TestedRecycler(5);

        var rand = new Random(10);

        await Task.WhenAll(Enumerable.Range(0, 10).Select(i => Task.Run(async () =>
        {
            var payload = TimeSpan.FromMilliseconds(rand.Next(100) * 10);
            var response = await _serverEmulator.ProcessRequestAsync(payload);
            Console.WriteLine($"{DateTime.Now} ===> Request processed with response {response}.");
        })).ToArray());
    }
}