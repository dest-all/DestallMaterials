namespace EverModern.Events;

/// <summary>
/// A unit type (empty struct) used as a type parameter for event sources
/// that do not publish meaningful data.
/// </summary>
public readonly struct Nothing();

/// <summary>
/// A thread-safe synchronous event source that accepts parameterless handlers.
/// </summary>
public class EventSource : BaseEventSource<Nothing>, INotifier
{
    /// <summary>
    /// Invokes all subscribed handlers.
    /// </summary>
    public void Invoke() => base.Invoke(default);

    /// <summary>
    /// Subscribes a parameterless handler. The returned <see cref="Subscription"/>
    /// should be disposed to unregister.
    /// </summary>
    /// <param name="handler">The handler to invoke on each notification.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
    public Subscription Subscribe(Action handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return Subscribe(_ => handler());
    }
}

/// <summary>
/// A thread-safe synchronous event source that publishes values of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The published value type.</typeparam>
public class EventSource<T> : BaseEventSource<T>, INotifier<T>
{
    /// <summary>
    /// Invokes all subscribed handlers with the specified value.
    /// </summary>
    /// <param name="newValue">The value to publish.</param>
    public new virtual void Invoke(T newValue) => base.Invoke(newValue);

    /// <summary>
    /// Subscribes a handler that receives published values.
    /// </summary>
    /// <param name="handler">The handler to invoke for each value.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
    public new Subscription Subscribe(Action<T> handler) => base.Subscribe(handler);
}

/// <summary>
/// Provides the common infrastructure for thread-safe synchronous event sources.
/// Handles subscription management, invocation, and disposal.
/// </summary>
/// <typeparam name="T">The published value type.</typeparam>
public abstract class BaseEventSource<T> : IDisposable
{
    readonly object _sync = new();

    Action<T>? _handlers;
    bool _disposed;

    /// <summary>
    /// Registers a handler. The returned <see cref="Subscription"/>
    /// should be disposed to unregister the handler.
    /// </summary>
    /// <param name="handler">The handler to invoke on each notification.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">The source has been disposed.</exception>
    protected Subscription Subscribe(Action<T> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);

        lock (_sync)
        {
            ThrowIfDisposed();
            _handlers += handler;
        }

        return new Subscription(() =>
            {
                lock (_sync)
                {
                    if (_disposed)
                        return;

                    _handlers -= handler;
                }
            }
        );
    }

    /// <summary>
    /// Invokes all subscribed handlers with the specified value.
    /// A snapshot of handlers is taken under the lock to allow safe reentrancy.
    /// </summary>
    /// <param name="value">The value to publish.</param>
    /// <exception cref="ObjectDisposedException">The source has been disposed.</exception>
    protected void Invoke(T value)
    {
        Action<T>? snapshot;

        lock (_sync)
        {
            ThrowIfDisposed();
            snapshot = _handlers;
        }

        snapshot?.Invoke(value);
    }

    /// <summary>
    /// Disposes the event source, unsubscribing all handlers and preventing further use.
    /// </summary>
    public void Dispose()
    {
        lock (_sync)
        {
            if (_disposed)
                return;

            _disposed = true;
            _handlers = null;
        }
    }

    void ThrowIfDisposed()
    {
        if (_disposed)
            throw new ObjectDisposedException(this.GetType().FullName);
    }
}
