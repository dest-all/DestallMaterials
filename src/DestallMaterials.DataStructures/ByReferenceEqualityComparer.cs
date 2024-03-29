﻿using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace DestallMaterials.WheelProtection.DataStructures;

public class ByReferenceEqualityComparer<T> : IEqualityComparer<T>
    where T : class
{
    ByReferenceEqualityComparer() { }

    public static readonly ByReferenceEqualityComparer<T> Instance =
        new ByReferenceEqualityComparer<T>();

    public bool Equals(T? x, T? y) => ReferenceEquals(x, y);

    public int GetHashCode([DisallowNull] T obj) => RuntimeHelpers.GetHashCode(obj);
}

public class DelegatingEqualityComparer<T> : IEqualityComparer<T>
{
    public required Func<T, T, bool> AreEqual { get; init; }

    public required Func<T, int> ComputeHashCode { get; init; }

    bool IEqualityComparer<T>.Equals(T? x, T? y) => AreEqual(x, y);

    int IEqualityComparer<T>.GetHashCode(T obj) => ComputeHashCode(obj);
}
