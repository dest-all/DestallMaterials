using DestallMaterials.WheelProtection.Extensions.Tasks;

namespace DestallMaterials.Tests;

public class ExtensionsTesting
{
    [Test]
    public async Task AwaitTuple()
    {
        var firstTask = Task.FromResult(1);
        var secondTask = Task.FromResult(2);
        var thirdTask = Task.FromResult(3);
        var fourthTask = Task.FromResult(4);

        var (first, second, third, fourth) = await (firstTask, secondTask, thirdTask, fourthTask);

        Assert.AreEqual(first + second + third + fourth, 10);
    }
}
