using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;
using Microsoft.Extensions.ObjectPool;

[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.NativeAot70)]
[MemoryDiagnoser]
[RPlotExporter]
public class DifferentInvocations
{
    readonly TunableStruct _struct = new();
    readonly SealedImplementor _implementor = new();
    readonly Implementor _regularImplementor = new();
    readonly BaseClass _baseImplementor = new Implementor();

    [Params(1000, 10000)]
    public int N;

    [GlobalSetup]
    public void Setup()
    {
       
    }

    [Benchmark]
    public string Struct_Delegate() => _struct.CalculateDelegate(N);

    [Benchmark]
    public string Struct_Direct() => _struct.CalculateIndependently(N);

    [Benchmark]
    public string Regular_Implementor() => _regularImplementor.Calculate(N);

    [Benchmark]
    public string Base_Implementor() => _baseImplementor.Calculate(N);

    [Benchmark]
    public string Sealed_Implementor() => _implementor.Calculate(N);
}

abstract class BaseClass
{
    public abstract string Calculate(int n);
}

class Implementor : BaseClass
{
    public static string UniversalCalc(int n) 
        => string.Join('.', Enumerable.Range(0, n));

    public override string Calculate(int n)
        => UniversalCalc(n); 
}

sealed class SealedImplementor : Implementor
{
}

struct TunableStruct
{
    public TunableStruct()
    {
    }

    public Func<int, string> CalculateDelegate { get; } = Implementor.UniversalCalc;

    public string CalculateIndependently(int n) => Implementor.UniversalCalc(n);
}

