using System.Collections.Generic;

namespace EverModern.SyntaxGenerator
{
    public class UtilityType
    {
        public UtilityType(string name, string @namespace, string code)
        {
            Name = name;
            Namespace = @namespace;
            Code = code;
        }

        public string Name { get; }
        public string Namespace { get; }
        public string Code { get; }

        public override string ToString()
            => $"{Namespace}.{Name}";
    }

    public static class UtilityTypes
    {
        public static UtilityType Nothing { get; } = new UtilityType("Nothing", "Destall", $@"namespace Destall
        {{
            public struct Nothing
            {{
            }}
        }}");

        public static UtilityType TaskTupleAwaiter { get; } = new UtilityType("TaskTupleAwaiter",
            "EverModern.Extensions.Tuples",
            $@"using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EverModern.Extensions.Tuples
{{
    public readonly struct TaskTupleAwaiter<T> : INotifyCompletion
    {{
        private readonly Task _task;
        private readonly Func<T> _getResult;

        internal TaskTupleAwaiter(Task task, Func<T> getResult)
        {{
            _task = task;
            _getResult = getResult;
        }}

        public bool IsCompleted => _task.IsCompleted;

        public T GetResult()
        {{
            _task.GetAwaiter().GetResult();
            return _getResult();
        }}

        public void OnCompleted(Action continuation) =>
            _task.GetAwaiter().OnCompleted(continuation);

        public void UnsafeOnCompleted(Action continuation) =>
            _task.GetAwaiter().UnsafeOnCompleted(continuation);
    }}
}}");

        public static IReadOnlyList<UtilityType> All = new UtilityType[]
        {
            Nothing,
            TaskTupleAwaiter
        };
    }
}

