using Microsoft.CodeAnalysis;

namespace EverModern.Memory;

[Generator]
public class MemoryPackSourceGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        AutoPackGenerator.Register(context);
    }
}
