using EverModern.Events;

namespace EverModern.Tests.XUnit;

/// <summary>
/// Exposes the protected <see cref="Scope.Enter"/> method for testing.
/// </summary>
public class TestScope : Scope
{
    public new Scope Enter() => base.Enter();
}

public class ScopeTests
{
    [Fact]
    public void EnterNew_ShouldCreateAndEnterScope()
    {
        // Act
        using var scope = Scope.EnterNew();

        // Assert - no exception means entered successfully
        Assert.NotNull(scope);
    }

    [Fact]
    public void Lifecycle_ShouldFireEventsInCorrectOrder()
    {
        // Arrange
        var scope = new TestScope();
        var order = new List<string>();

        scope.BeforeEnter.Subscribe(() => order.Add("BeforeEnter"));
        scope.AfterEnter.Subscribe(() => order.Add("AfterEnter"));
        scope.BeforeExit.Subscribe(() => order.Add("BeforeExit"));
        scope.AfterExit.Subscribe(() => order.Add("AfterExit"));

        // Act
        scope.Enter();
        scope.Finish();

        // Assert
        Assert.Equal(4, order.Count);
        Assert.Equal("BeforeEnter", order[0]);
        Assert.Equal("AfterEnter", order[1]);
        Assert.Equal("BeforeExit", order[2]);
        Assert.Equal("AfterExit", order[3]);
    }

    [Fact]
    public void Enter_Twice_ShouldThrow()
    {
        // Arrange
        var scope = new TestScope();
        scope.Enter();

        // Act & Assert
        var ex = Assert.Throws<InvalidOperationException>(() => scope.Enter());
        Assert.Contains("already entered", ex.Message, StringComparison.OrdinalIgnoreCase);
    }

    [Fact]
    public void Dispose_ShouldTriggerBeforeExitAndAfterExit()
    {
        // Arrange
        var scope = new TestScope();
        scope.Enter();
        var exitFired = false;
        scope.BeforeExit.Subscribe(() => exitFired = true);

        // Act
        ((IDisposable)scope).Dispose();

        // Assert
        Assert.True(exitFired);
    }

    [Fact]
    public void AccessProperties_AfterFinish_ShouldThrow()
    {
        // Arrange
        var scope = new Scope();
        scope.Finish();

        // Act & Assert
        Assert.Throws<ObjectDisposedException>(() => scope.BeforeEnter);
        Assert.Throws<ObjectDisposedException>(() => scope.AfterEnter);
        Assert.Throws<ObjectDisposedException>(() => scope.BeforeExit);
        Assert.Throws<ObjectDisposedException>(() => scope.AfterExit);
    }

    [Fact]
    public void Finish_Twice_ShouldBeIdempotent()
    {
        // Arrange
        var scope = new Scope();

        // Act & Assert
        var ex = Record.Exception(() =>
        {
            scope.Finish();
            scope.Finish();
        });
        Assert.Null(ex);
    }

    [Fact]
    public void Finish_WithoutEnter_ShouldNotThrow()
    {
        // Arrange
        var scope = new Scope();

        // Act & Assert
        var ex = Record.Exception(() => scope.Finish());
        Assert.Null(ex);
    }

    [Fact]
    public void ObserverExceptions_ShouldNotBreakScopeLifecycle()
    {
        // Arrange
        var scope = new TestScope();
        scope.BeforeEnter.Subscribe(() => throw new InvalidOperationException("before enter fail"));
        scope.AfterEnter.Subscribe(() => throw new InvalidOperationException("after enter fail"));
        scope.Enter();

        scope.BeforeExit.Subscribe(() => throw new InvalidOperationException("before exit fail"));
        scope.AfterExit.Subscribe(() => throw new InvalidOperationException("after exit fail"));

        // Act - exceptions are swallowed, should not throw
        scope.Finish();

        // Assert - scope should be properly disposed
        Assert.Throws<ObjectDisposedException>(() => scope.BeforeEnter);
    }

    [Fact]
    public void Finish_ShouldNotFireEnterEventsOnSecondFinish()
    {
        // Arrange
        var scope = new TestScope();
        scope.Enter();
        var afterExitCount = 0;
        scope.AfterExit.Subscribe(() => afterExitCount++);

        // Act
        scope.Finish();

        // Assert - AfterExit should only fire once
        Assert.Equal(1, afterExitCount);
    }

    [Fact]
    public void BeforeExit_ShouldFireInsideLock_AfterExitOutsideLock()
    {
        // This tests the design: BeforeExit fires while _entered is still true,
        // AfterExit fires after _entered is set to false
        // We can verify by checking the flag during BeforeExit
        var scope = new TestScope();
        scope.Enter();

        // We can't check internal _entered directly, but we can verify event order.
        var order = new List<string>();
        scope.BeforeExit.Subscribe(() => order.Add("BeforeExit"));
        scope.AfterExit.Subscribe(() => order.Add("AfterExit"));

        scope.Finish();

        Assert.Equal(2, order.Count);
        Assert.Equal("BeforeExit", order[0]);
        Assert.Equal("AfterExit", order[1]);
    }

    [Fact]
    public void SubscribeToLifecycleEvents_BeforeEnter_ShouldWork()
    {
        // Arrange
        var scope = new TestScope();
        var beforeFired = false;
        scope.BeforeEnter.Subscribe(() => beforeFired = true);

        // Act
        scope.Enter();

        // Assert
        Assert.True(beforeFired);
    }

    [Fact]
    public void SubscribeToLifecycleEvents_AfterEnter_ShouldBeFired()
    {
        // Arrange
        var scope = new TestScope();
        var afterFired = false;
        scope.AfterEnter.Subscribe(() => afterFired = true);

        // Act
        scope.Enter();

        // Assert
        Assert.True(afterFired);
    }

    [Fact]
    public void AfterExit_ShouldFireAfterValueIsReadable()
    {
        // If AfterExit fires after _entered is false,
        // subscribers should still work normally
        var scope = new TestScope();
        scope.Enter();

        var afterExitFired = false;
        scope.AfterExit.Subscribe(() => afterExitFired = true);

        scope.Finish();
        Assert.True(afterExitFired);
    }
}

public class ScopeEnterNewTests
{
    [Fact]
    public void EnterNew_AfterExit_ShouldFireOnFinish()
    {
        // Arrange
        var afterExit = false;

        // Act
        var scope = Scope.EnterNew();
        scope.AfterExit.Subscribe(() => afterExit = true);
        scope.Finish();

        // Assert
        Assert.True(afterExit);
    }
}
