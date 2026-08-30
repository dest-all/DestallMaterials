using EverModern.Events;

namespace EverModern.Tests.XUnit;

public class SubscriptionTests
{
    [Fact]
    public void Dispose_ShouldInvokeOnDisposedCallback()
    {
        // Arrange
        var invoked = false;
        var sub = new Subscription(() => invoked = true);

        // Act
        sub.Dispose();

        // Assert
        Assert.True(invoked);
    }

    [Fact]
    public void Dispose_ShouldPassSelfToCallback()
    {
        // Arrange
        Subscription? captured = null;
        var sub = new Subscription(s => captured = s);

        // Act
        sub.Dispose();

        // Assert
        Assert.Same(sub, captured);
    }

    [Fact]
    public void Dispose_Twice_ShouldInvokeCallbackOnlyOnce()
    {
        // Arrange
        var invokeCount = 0;
        var sub = new Subscription(() => invokeCount++);

        // Act
        sub.Dispose();
        sub.Dispose();

        // Assert
        Assert.Equal(1, invokeCount);
    }

    [Fact]
    public void Dispose_AfterFirstCall_ShouldBeIdempotent()
    {
        // Arrange
        var invokeCount = 0;
        var sub = new Subscription(() => invokeCount++);

        // Act
        sub.Dispose();
        sub.Dispose();
        sub.Dispose();

        // Assert
        Assert.Equal(1, invokeCount);
    }

    [Fact]
    public void Constructor_WithAction_SelfCallback_ShouldWork()
    {
        // Arrange
        Subscription? captured = null;
        var sub = new Subscription(onDisposed: s => captured = s);

        // Act
        sub.Dispose();

        // Assert
        Assert.Same(sub, captured);
    }

    [Fact]
    public void Dispose_ShouldBeReentrantSafe()
    {
        // Arrange
        Subscription? innerSub = null;
        var sub = new Subscription(() => innerSub?.Dispose());

        // Act - should not throw
        var ex = Record.Exception(() => sub.Dispose());

        // Assert
        Assert.Null(ex);
    }
}
