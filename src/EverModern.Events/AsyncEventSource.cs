namespace EverModern.Events;

/// <summary>
/// A thread-safe asynchronous event source that accepts parameterless handlers.
/// </summary>
public sealed class AsyncEventSource : BaseAsyncEventSource<Nothing>, IAsyncNotifier
{
    /// <summary>
    /// Invokes all subscribed asynchronous handlers.
    /// </summary>
    public ValueTask InvokeAsync() => base.InvokeAsync(default);

    /// <summary>
    /// Subscribes a parameterless asynchronous handler.
    /// </summary>
    /// <param name="handler">The handler to invoke on each notification.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
    public Subscription Subscribe(Func<ValueTask> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return base.Subscribe(_ => handler());
    }
}

/// <summary>
/// A thread-safe asynchronous event source that publishes values of type <typeparamref name="T"/>.
/// </summary>
/// <typeparam name="T">The published value type.</typeparam>
public sealed class AsyncEventSource<T> : BaseAsyncEventSource<T>, IAsyncNotifier<T>
{
    /// <summary>
    /// Invokes all subscribed handlers with the specified value.
    /// </summary>
    /// <param name="value">The value to publish.</param>
    public new ValueTask InvokeAsync(T value) => base.InvokeAsync(value);

    /// <summary>
    /// Subscribes a handler that receives published values.
    /// </summary>
    /// <param name="handler">The handler to invoke for each value.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    public new Subscription Subscribe(Func<T, ValueTask> handler)
        => base.Subscribe(handler);
}

/// <summary>
/// Provides the common infrastructure for thread-safe asynchronous event sources.
/// Handles subscription management, invocation, and disposal.
/// </summary>
/// <typeparam name="T">The published value type.</typeparam>
public abstract class BaseAsyncEventSource<T> : IDisposable
{
    readonly object _sync = new();

    Func<T, ValueTask>? _handlers;
    bool _disposed;

    /// <summary>
    /// Registers an asynchronous handler. The returned <see cref="Subscription"/>
    /// should be disposed to unregister the handler.
    /// </summary>
    /// <param name="handler">The handler to invoke on each notification.</param>
    /// <returns>A <see cref="Subscription"/> that unsubscribes when disposed.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="handler"/> is <see langword="null"/>.</exception>
    /// <exception cref="ObjectDisposedException">The source has been disposed.</exception>
    protected Subscription Subscribe(Func<T, ValueTask> handler)
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
    /// Handlers are executed sequentially in subscription order.
    /// A snapshot of handlers is taken under the lock to allow safe reentrancy.
    /// </summary>
    /// <param name="value">The value to publish.</param>
    /// <exception cref="ObjectDisposedException">The source has been disposed.</exception>
    protected async ValueTask InvokeAsync(T value)
    {
        Func<T, ValueTask>? snapshot;

        lock (_sync)
        {
            ThrowIfDisposed();
            snapshot = _handlers;
        }

        if (snapshot is null)
            return;

        foreach (Func<T, ValueTask> handler in snapshot.GetInvocationList())
        {
            await handler(value).ConfigureAwait(false);
        }
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
            throw new ObjectDisposedException(GetType().Name);
    }
}
