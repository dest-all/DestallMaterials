using DestallMaterials.WheelProtection.DataStructures;
using DestallMaterials.WheelProtection.DataStructures.Buffers;
using DestallMaterials.WheelProtection.Extensions.Ranges;
using DestallMaterials.WheelProtection.Linq;

namespace DestallMaterials.Tests;

public class DataStructuresTesting
{
    [Test]
    public void TypeCheckingWithNothing()
    {
        Assert.True(CheckIfType<string, string>());
        Assert.False(CheckIfType<int, string>());

        Assert.True(Enumerable.Empty<int>() is IEnumerable<object>);

        Assert.True(CheckIfType<int, object>());
    }

    static bool CheckIfType<T1, T2>()
    {
        Nothing<T1> nt1 = default;

        return nt1 switch
        {
            Nothing<T2> => true,
            _ => false
        };
    }
}