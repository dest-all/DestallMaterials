namespace EverModern.Events;

/// <summary>
/// Represents a notifier that exposes its current value and publishes changes.
/// </summary>
/// <typeparam name="T">The value type.</typeparam>
public interface IValueNotifier<out T> : INotifier<T>
{
    /// <summary>
    /// Gets the current value.
    /// </summary>
    T Value { get; }

    /// <summary>
    /// Susbsribes and calls the handler after the new value has been set.
    /// </summary>
    /// <param name="handler"></param>
    /// <returns></returns>
    Subscription SubscribeAfter(Action handler);

    /// <summary>
    /// Subscribes a handler that receives published values and its own subscription handle.
    /// </summary>
    /// <param name="handler">The handler to invoke for each value.</param>
    void SubscribeAfter(Action<Subscription> handler)
    {
        Subscription subscription = null!;
        Action actualHandler = () => handler(subscription);
        subscription = SubscribeAfter(actualHandler);
    }
}
