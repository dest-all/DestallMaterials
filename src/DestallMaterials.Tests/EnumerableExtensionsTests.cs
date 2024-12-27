using DestallMaterials.WheelProtection.Extensions.Enumerables;
using DestallMaterials.WheelProtection.Extensions.Tasks;

namespace DestallMaterials.Tests;

public class EnumerableExtensionsTests
{
    readonly List<string> _output = new List<string>();

    [SetUp]
    public void Setup()
    {
    }


    [Test]
    public void TestSplit()
    {
        var result = Enumerable.Range(0, 100).Split(10).Select(e=>e.ToArray());
    }

    [Test]

    public async Task AwaitErroredTuple()
    {
        Assert.ThrowsAsync<TaskCanceledException>(async () =>
        {
            var (a, b) = await (Task.FromResult(100), Task.Run(() => { throw new TaskCanceledException(); return 15; }));

            var c = a + b;
        });
    }

    [Test]
    public async Task AwaitErroredTasksList()
    {
        var tasks = Enumerable.Range(0, 10).SelectAsync(async i =>
        {
            await Task.Delay(TimeSpan.FromMilliseconds(i * 100));
            throw new TaskCanceledException();
            return 0;
        });

        Assert.ThrowsAsync<AggregateException>(() => tasks.ToListAsync());
    }
}