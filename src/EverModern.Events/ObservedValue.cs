namespace EverModern.Events;

/// <summary>
/// An observable value that publishes its changes to subscribers.
/// When <see cref="Change"/> is called, all handlers are invoked with the new value,
/// then the internal value is updated.
/// </summary>
/// <typeparam name="TValue">The value type.</typeparam>
public class ObservedValue<TValue>(TValue value, IEqualityComparer<TValue> comparer)
    : IValueNotifier<TValue>, IDisposable
{
    readonly EventSource _afterEventSource = new();
    readonly EventSource<TValue> _beforeEventSource = new();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="value"></param>
    public ObservedValue(TValue value)
        : this(value, EqualityComparer<TValue>.Default) { }

    /// <summary>
    /// Gets the current value.
    /// </summary>
    public TValue Value { get; private set; } = value;

    /// <summary>
    /// Updates the value and notifies all subscribers with the new value.
    /// Notification occurs before the internal value is updated.
    /// </summary>
    /// <param name="newValue">The new value.</param>
    public void Change(TValue newValue)
    {
        if (comparer.Equals(Value, newValue))
            return;

        _beforeEventSource.Invoke(newValue);
        Value = newValue;
        _afterEventSource.Invoke();
    }

    /// <inheritdoc/>
    public void Dispose()
    {
        ((IDisposable)_afterEventSource).Dispose();
        ((IDisposable)_beforeEventSource).Dispose();
    }

    /// <summary>
    /// Subscribes a handler that receives the latest published value on each change.
    /// </summary>
    /// <param name="handler">The handler to invoke when the value changes.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    public Subscription Subscribe(Action<TValue> handler) => _beforeEventSource.Subscribe(handler);

    /// <inheritdoc/>
    public Subscription SubscribeAfter(Action handler) => _afterEventSource.Subscribe(handler);
}
