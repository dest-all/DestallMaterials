using DestallMaterials.WheelProtection.DataStructures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.Tests
{
    public class EqualityComparer
    {
        [Test]
        public void GetHashCode_MustIgnoreOverriden()
        {
            var equalityComparer = new ByReferenceEqualityComparer<OverridingClass>();

            var item1 = new OverridingClass
            {
                Value = 1
            };

            var item2 = new OverridingClass
            {
                Value = 1
            };

            Assert.AreEqual(item1, item2);
            Assert.AreEqual(item1.GetHashCode(), item2.GetHashCode());

            Assert.AreNotEqual(equalityComparer.GetHashCode(item1), equalityComparer.GetHashCode(item2));
            Assert.IsFalse(equalityComparer.Equals(item1, item2));
        }

        class OverridingClass
        {
            public int Value { get; set; }
            public override int GetHashCode()
                => Value;

            public override bool Equals(object? obj)
                => obj is OverridingClass other && other.Value == Value;
        }
    }
}
