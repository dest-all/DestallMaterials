namespace EverModern.Events;

/// <summary>
/// Exposes lifecycle event sources for a <see cref="Scope"/>.
/// </summary>
public interface IObservableScope
{
    /// <summary>
    /// Gets the event source that fires immediately before the scope is entered.
    /// </summary>
    INotifier BeforeEnter { get; }

    /// <summary>
    /// Gets the event source that fires immediately after the scope is entered.
    /// </summary>
    INotifier AfterEnter { get; }

    /// <summary>
    /// Gets the event source that fires immediately before the scope is exited.
    /// </summary>
    INotifier BeforeExit { get; }

    /// <summary>
    /// Gets the event source that fires immediately after the scope is exited.
    /// </summary>
    INotifier AfterExit { get; }
}

/// <summary>
/// Represents a named lifecycle scope with observable enter/exit events.
/// Use <see cref="EnterNew"/> to create and enter a scope, then call <see cref="Finish"/>
/// or dispose to exit and clean up.
/// </summary>
public class Scope : IObservableScope, IDisposable
{
    readonly object _locker = new();

    readonly EventSource _beforeEnter = new();
    readonly EventSource _afterEnter = new();
    readonly EventSource _beforeExit = new();
    readonly EventSource _afterExit = new();

    bool _entered;
    bool _disposed;

    /// <summary>
    /// Gets the event source that fires immediately before the scope is entered.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The scope has been disposed.</exception>
    public INotifier BeforeEnter => ThrowIfDisposed(_beforeEnter);

    /// <summary>
    /// Gets the event source that fires immediately after the scope is entered.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The scope has been disposed.</exception>
    public INotifier AfterEnter => ThrowIfDisposed(_afterEnter);

    /// <summary>
    /// Gets the event source that fires immediately before the scope is exited.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The scope has been disposed.</exception>
    public INotifier BeforeExit => ThrowIfDisposed(_beforeExit);

    /// <summary>
    /// Gets the event source that fires immediately after the scope is exited.
    /// </summary>
    /// <exception cref="ObjectDisposedException">The scope has been disposed.</exception>
    public INotifier AfterExit => ThrowIfDisposed(_afterExit);

    /// <summary>
    /// Creates a new scope and immediately enters it.
    /// </summary>
    /// <returns>The entered scope.</returns>
    public static Scope EnterNew()
    {
        var scope = new Scope();
        scope.Enter();
        return scope;
    }

    /// <summary>
    /// Enters the scope, firing <see cref="BeforeEnter"/> and <see cref="AfterEnter"/>.
    /// </summary>
    /// <returns>This scope instance.</returns>
    /// <exception cref="InvalidOperationException">The scope has already been entered.</exception>
    /// <exception cref="ObjectDisposedException">The scope has been disposed.</exception>
    protected Scope Enter()
    {
        lock (_locker)
        {
            ThrowIfDisposedLocked();

            if (_entered)
                throw new InvalidOperationException("Scope already entered.");

            SafeInvoke(_beforeEnter);
            _entered = true;
        }

        SafeInvoke(_afterEnter);
        return this;
    }

    /// <summary>
    /// Exits the scope, firing <see cref="BeforeExit"/> and <see cref="AfterExit"/>,
    /// then disposes all internal event sources.
    /// If the scope is already disposed, this is a no-op.
    /// </summary>
    public void Finish()
    {
        EventSource beforeExit, afterExit;

        lock (_locker)
        {
            if (_disposed)
                return;

            _disposed = true;

            if (!_entered)
            {
                DisposeSources();
                return;
            }

            beforeExit = _beforeExit;
            afterExit = _afterExit;
        }

        // Outside lock: callbacks must not block state transitions
        SafeInvoke(beforeExit);

        lock (_locker)
        {
            _entered = false;
        }

        SafeInvoke(afterExit);

        DisposeSources();
    }

    void DisposeSources()
    {
        ScopeExtensions.DisposeAll(
            _beforeEnter,
            _afterEnter,
            _beforeExit,
            _afterExit
        );
    }

    static void SafeInvoke(EventSource source)
    {
        try
        {
            source.Invoke();
        }
        catch
        {
            // swallow or optionally log
            // important: lifecycle must not break due to observers
        }
    }

    void ThrowIfDisposedLocked()
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(Scope));
    }

    T ThrowIfDisposed<T>(T value)
    {
        if (_disposed)
            throw new ObjectDisposedException(nameof(Scope));

        return value;
    }

    void IDisposable.Dispose()
        => Finish();
}
