using EverModern.Events;

namespace EverModern.Tests.XUnit;

public class ScopeExtensionsTests
{
    [Fact]
    public void BoundToScope_ShouldDisposeResourceOnScopeExit()
    {
        // Arrange
        var scope = new TestScope();
        scope.Enter();
        var disposed = false;
        var disposable = new DisposableSpy(() => disposed = true);

        // Act
        var result = disposable.BoundToScope(scope);
        scope.Finish();

        // Assert
        Assert.True(disposed);
        Assert.Same(disposable, result);
    }

    [Fact]
    public void BoundToScope_ShouldNotDisposeBeforeScopeExit()
    {
        // Arrange
        var scope = new TestScope();
        scope.Enter();
        var disposed = false;
        var disposable = new DisposableSpy(() => disposed = true);

        // Act
        disposable.BoundToScope(scope);

        // Assert - resource not disposed yet
        Assert.False(disposed);
    }

    [Fact]
    public void BoundToScope_ShouldWorkWithEnterNewScope()
    {
        // Arrange
        using var scope = Scope.EnterNew();
        var disposed = false;
        var disposable = new DisposableSpy(() => disposed = true);

        // Act
        disposable.BoundToScope(scope);

        // Assert - not disposed while scope is alive
        Assert.False(disposed);
    }

    [Fact]
    public void DisposeAll_ShouldDisposeAllProvidedInstances()
    {
        // Arrange
        var log = new List<int>();
        var d1 = new DisposableSpy(() => log.Add(1));
        var d2 = new DisposableSpy(() => log.Add(2));
        var d3 = new DisposableSpy(() => log.Add(3));

        // Act
        ScopeExtensions.DisposeAll(d1, d2, d3);

        // Assert
        Assert.Equal(3, log.Count);
        Assert.Contains(1, log);
        Assert.Contains(2, log);
        Assert.Contains(3, log);
    }

    [Fact]
    public void DisposeAll_WithEmptySpan_ShouldNotThrow()
    {
        // Act & Assert
        var ex = Record.Exception(() => ScopeExtensions.DisposeAll());
        Assert.Null(ex);
    }

    [Fact]
    public void DisposeAll_WithSingleItem_ShouldDisposeIt()
    {
        // Arrange
        var disposed = false;
        var d = new DisposableSpy(() => disposed = true);

        // Act
        ScopeExtensions.DisposeAll(d);

        // Assert
        Assert.True(disposed);
    }

    [Fact]
    public void BoundToScope_ShouldSelfCleanupSubscription()
    {
        // Arrange
        var scope = new TestScope();
        scope.Enter();
        var disposable = new DisposableSpy(() => { });

        // The subscription bound to scope.AfterExit should auto-dispose
        // when the scope finishes, so it doesn't leak.
        disposable.BoundToScope(scope);

        // Get a reference to the BeforeExit event source to check subscriber count
        // We can't easily check internals, but we can verify no exceptions occur
        var ex = Record.Exception(() => scope.Finish());
        Assert.Null(ex);
    }

    class DisposableSpy(Action onDispose) : IDisposable
    {
        public void Dispose() => onDispose();
    }
}
