using Microsoft.Extensions.Logging;

namespace DestallMaterials.CodeGeneration.Environment;

class ConsoleLogger : ILogger
{
    class Scope : IDisposable
    {
        public void Dispose()
        {
        }
    }
    public IDisposable BeginScope<TState>(TState state)
    {
        return new Scope();
    }

    public bool IsEnabled(LogLevel logLevel) => true;

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = formatter(state, exception);
        Console.WriteLine(message);
    }
}
