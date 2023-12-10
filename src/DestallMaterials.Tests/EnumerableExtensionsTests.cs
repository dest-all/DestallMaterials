using DestallMaterials.WheelProtection.Extensions.Enumerables;

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
        public void TestSplit()
        {
            var result = Enumerable.Range(0, 100).Split(10).Select(e=>e.ToArray());
        }
    }
}