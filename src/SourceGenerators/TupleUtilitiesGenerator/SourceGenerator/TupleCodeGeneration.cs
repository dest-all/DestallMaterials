using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static System.Linq.Enumerable;


namespace EverModern.SyntaxGenerator;

public static class TupleCodeGeneration
{
    public const string ExtensionClassName = "TupleExtensions";
    public const string ExtensionNamespace = "EverModern.Extensions.Tuples";
    const string _prioritise = "[System.Runtime.CompilerServices.OverloadResolutionPriority(1)]";

    public static StringBuilder GenerateExtensionClass(this IEnumerable<TupleExpressionSyntax> tupleSyntaxes)
    {
        var uniqueArities = tupleSyntaxes
            .Select(t => t.Arguments.Count)
            .Where(count => count > 0)
            .Distinct()
            .ToArray();

        var extensionMethods = new
        {
            General = uniqueArities
                .SelectMany(arity => TupleCodeGeneration.ComposeLinqExtensions(arity))
                .ToArray()
        };


        var result = new StringBuilder($"namespace {ExtensionNamespace}\n{{\n\tpublic static partial class {ExtensionClassName}\n{{\n\t\t");

        foreach (var method in extensionMethods.General)
        {
            result.AppendLine(method);
        }

        result.AppendLine("\t}\n}");

        return result;
    }

    static IEnumerable<string> ComposeGenericTaskTupleGetAwaiters(int arity)
    {
        var ts = Range(1, arity).Select(i => $"T{i}").Merge();
        var taskArgs = Range(1, arity).Select(i => $"System.Threading.Tasks.Task<T{i}>").Merge();
        var whenAllArgs = Range(1, arity).Select(i => $"tasks.Item{i}").Merge();
        var resultExprs = Range(1, arity).Select(i => $"tasks.Item{i}.Result").Merge();

        yield return $@"{_prioritise}
    public static TaskTupleAwaiter<({ts})> GetAwaiter<{ts}>(this ({taskArgs}) tasks)
        => new(System.Threading.Tasks.Task.WhenAll({whenAllArgs}), () => ({resultExprs}));";
    }

    /// <summary>Pre-generated generic GetAwaiter methods for arities 2–7, always emitted
    /// so that tuple-of-task types formed via .Select() or method returns can be awaited.</summary>
    public static string PreGeneratedGenericAwaiters
        => string.Join("\n", Range(2, 6).SelectMany(ComposeGenericTaskTupleGetAwaiters));

    public static IEnumerable<string> MakeTupleExtensionMethods(this INamedTypeSymbol tuple)
    {
        if (tuple.TupleElements.Length == 0)
        {
            yield break;
        }

        var linq = ComposeLinqExtensions(tuple.TupleElements.Length);
        var taskMethod = TaskExtensionMethod(tuple);

        if (taskMethod != null)
        {
            yield return taskMethod;
        }

        foreach (var method in linq)
        {
            yield return method;
        }
    }

    enum TaskVariant
    {
        Task, ValueTask
    }

    static string Of(this TaskVariant taskVariant, string returnType)
    {
        var result = taskVariant == TaskVariant.Task ? taskTypeSignature : valueTaskTypeSignature;

        if (returnType != null)
        {
            result += $"<{returnType}>";
        }

        return result;
    }


    const string taskTypeSignature = "System.Threading.Tasks.Task";
    const string valueTaskTypeSignature = "System.Threading.Tasks.ValueTask";
    static (bool IsTask, TaskVariant Variant, ITypeSymbol ReturnType) IsTask(this ITypeSymbol type)
    {
        var nts = type as INamedTypeSymbol;
        if (nts is null)
        {
            return default;
        }
        var displayString = nts.ToDisplayString();

        var isReferenceTask = displayString.StartsWith(taskTypeSignature);
        var isValueTask = displayString.StartsWith(valueTaskTypeSignature);

        if (isReferenceTask || isValueTask)
        {
            var variant = isReferenceTask ? TaskVariant.Task : TaskVariant.ValueTask;
            var typeArguments = nts.TypeArguments;
            if (typeArguments.Length > 1)
            {
                return default;
            }

            return (true, variant, typeArguments.FirstOrDefault());
        }

        return default;
    }


    static IEnumerable<string> ComposeSumExtensions(INamedTypeSymbol numbersTuple)
    {
        yield break;
    }

    static IEnumerable<string> ComposeLinqExtensions(int elementsCount)
    {
        var ts = Repeat("T", elementsCount).Merge();
        var touts = Repeat("TOut", elementsCount).Merge();

        yield return $@"{_prioritise}
    public static System.Collections.Generic.IEnumerable<T> AsEnumerable<T>(this ({ts}) items)
            {{
                {Range(1, elementsCount).Select(n => $"yield return items.Item{n};").Merge("\n")}
            }}";
        yield return $@"{_prioritise}
    public static T[] ToArray<T>(this ({ts}) items)
                => System.Linq.Enumerable.ToArray(items.AsEnumerable());";
        yield return $@"{_prioritise}
    public static ({touts}) Select<T, TOut>(this ({ts}) items, System.Func<T, TOut> selector)
                    => ({Range(1, elementsCount).Select(i => $"selector(items.Item{i})").Merge()});";
        yield return $@"{_prioritise}
    public static bool Any<T>(this ({ts}) items, System.Func<T, bool> selector)
                    => ({Range(1, elementsCount).Select(i => $"selector(items.Item{i})").Merge(" || ")});";
        yield return $@"{_prioritise}
    public static bool All<T>(this ({ts}) items, System.Func<T, bool> selector)
                    => ({Range(1, elementsCount).Select(i => $"selector(items.Item{i})").Merge(" && ")});";
        yield return $@"{_prioritise}
    public static T First<T>(this ({ts}) items, System.Func<T, bool> selector)
            {{
                {Range(1, elementsCount).Select(i => $"if (selector(items.Item{i})) {{ return items.Item{i}; }}").Merge("\n")}
                throw new System.InvalidOperationException(""No elements in the multitude match the predicate."");
            }}";
        yield return $@"{_prioritise}
    public static T FirstOrDefault<T>(this ({ts}) items, System.Func<T, bool> selector)
            {{
                {Range(1, elementsCount).Select(i => $"if (selector(items.Item{i})) {{ return items.Item{i}; }}").Merge("\n")}
                return default;
            }}";
        yield return $@"{_prioritise}
    public static int Count<T>(this ({ts}) items, System.Func<T, bool> selector)
            {{
                int result = 0;
                {Range(1, elementsCount).Select(i => $"if (selector(items.Item{i})) {{ result++; }}").Merge("\n")}
                return result;
            }}";

        yield return $@"{_prioritise}
    public static System.Collections.Generic.IEnumerator<T> GetEnumerator<T>(this ({ts}) items)
            {{
                {Range(1, elementsCount).Select(i => $"yield return items.Item{i};").Merge("\n")}
            }}";

        if (elementsCount % 2 == 0)
        {
            var half = elementsCount / 2;
            yield return $@"{_prioritise}
    public static System.Collections.Generic.Dictionary<TKey, TValue> ToDictionary<TKey, TValue>(
                    this ({Range(0, elementsCount / 2).Select(_ => $"TKey, TValue").Merge()}) items)
                => new System.Collections.Generic.Dictionary<TKey, TValue>({half})
                {{
                    {Range(1, half).Select(i => $"[items.Item{i * 2 - 1}] = items.Item{i * 2}").Merge(",\n")}
                }};";
        }
    }

    static IEnumerable<string> ComposeTasksTupleExtensionsSyntax(
        this INamedTypeSymbol tupleSymbol)
    {
        var method = GenerateTaskTupleGetAwaiter(tupleSymbol);
        if (method != null)
            yield return method;
    }

    /// <summary>
    /// Generates a GetAwaiter extension method for a tuple where every element is a Task/ValueTask.
    /// Non-generic tasks are awaited but excluded from the result tuple.
    /// </summary>
    public static string GenerateTaskTupleGetAwaiter(INamedTypeSymbol tasksTuple)
    {
        var elements = tasksTuple.TupleElements;
        if (elements.Length == 0)
            return null;

        var info = elements.Select((e, i) =>
        {
            var (isTask, variant, returnType) = e.Type.IsTask();
            return (Index: i + 1, IsTaskOrValueTask: isTask, Variant: variant, ReturnType: returnType);
        }).ToArray();

        // All elements must be Task or ValueTask
        if (!info.All(x => x.IsTaskOrValueTask))
            return null;
        // At least one must have a return type
        if (!info.Any(x => x.ReturnType != null))
            return null;

        // Elements contributing to the result tuple (only generic ones)
        var resultElements = info.Where(x => x.ReturnType != null).ToArray();
        var resultTypes = resultElements.Select(x => x.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        var resultType = resultElements.Length == 1
            ? resultTypes.Single()
            : $"({string.Join(", ", resultTypes)})";

        // Input tuple type
        var inputTypes = info.Select(x =>
        {
            var baseType = x.Variant == TaskVariant.Task
                ? "System.Threading.Tasks.Task"
                : "System.Threading.Tasks.ValueTask";
            return x.ReturnType != null
                ? $"{baseType}<{x.ReturnType.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat)}>"
                : baseType;
        }).ToArray();
        var inputType = inputTypes.Length == 1
            ? inputTypes[0]
            : $"({string.Join(", ", inputTypes)})";

        // WhenAll arguments: .AsTask() for ValueTask variants
        var whenAllArgs = info.Select(x =>
        {
            var item = $"tasks.Item{x.Index}";
            return x.Variant == TaskVariant.ValueTask ? $"{item}.AsTask()" : item;
        });

        // Result factory: extract .Result from generic tasks only
        var resultExprs = resultElements.Select(x => $"tasks.Item{x.Index}.Result").ToArray();
        var resultFactory = resultElements.Length == 1
            ? resultExprs[0]
            : $"({string.Join(", ", resultExprs)})";

        return $@"{_prioritise}
    public static TaskTupleAwaiter<{resultType}> GetAwaiter(this {inputType} tasks)
        => new(System.Threading.Tasks.Task.WhenAll({string.Join(", ", whenAllArgs)}), () => {resultFactory});";
    }

    static string TaskExtensionMethod(INamedTypeSymbol tasksTuple)
        => GenerateTaskTupleGetAwaiter(tasksTuple);
}

