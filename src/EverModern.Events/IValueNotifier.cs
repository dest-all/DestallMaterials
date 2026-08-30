namespace EverModern.Events;

public interface IValueNotifier<out T> : INotifier<T>
{
    T Value { get; }
}
