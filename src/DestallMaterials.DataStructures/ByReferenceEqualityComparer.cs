using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DestallMaterials.WheelProtection.DataStructures
{
    public class ByReferenceEqualityComparer<T> : IEqualityComparer<T>
        where T : class
    {
        ByReferenceEqualityComparer() { }

        public static readonly ByReferenceEqualityComparer<T> Instance =
            new ByReferenceEqualityComparer<T>();

        public bool Equals(T? x, T? y) => object.ReferenceEquals(x, y);

        public int GetHashCode([DisallowNull] T obj) => RuntimeHelpers.GetHashCode(obj);
    }

    public class DelegatingEqualityComparer<T> : IEqualityComparer<T>
    {
        public required Func<T, T, bool> AreEqual { get; init; }

        public required Func<T, int> ComputeHashCode { get; init; }

        bool IEqualityComparer<T>.Equals(T? x, T? y) => AreEqual(x, y);

        int IEqualityComparer<T>.GetHashCode(T obj) => ComputeHashCode(obj);
    }
}
