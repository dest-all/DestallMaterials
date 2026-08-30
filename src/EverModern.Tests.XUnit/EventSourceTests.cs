using EverModern.Events;

namespace EverModern.Tests.XUnit;

public class EventSourceTests
{
    [Fact]
    public void Invoke_ShouldCallSubscriber()
    {
        var source = new EventSource();
        var invoked = false;
        source.Subscribe(() => invoked = true);

        source.Invoke();

        Assert.True(invoked);
    }

    [Fact]
    public void Subscribe_WithNullHandler_ShouldThrow()
    {
        var source = new EventSource();
        Assert.Throws<ArgumentNullException>(() => source.Subscribe((Action?)null!));
    }

    [Fact]
    public void Unsubscribe_ShouldStopReceivingNotifications()
    {
        var source = new EventSource();
        var invokeCount = 0;
        var sub = source.Subscribe(() => invokeCount++);

        source.Invoke();
        sub.Dispose();
        source.Invoke();

        Assert.Equal(1, invokeCount);
    }

    [Fact]
    public void MultipleSubscribers_ShouldAllBeInvoked()
    {
        var source = new EventSource();
        var count = 0;
        source.Subscribe(() => count++);
        source.Subscribe(() => count++);
        source.Subscribe(() => count++);

        source.Invoke();

        Assert.Equal(3, count);
    }

    [Fact]
    public void Dispose_ShouldUnsubscribeAll()
    {
        var source = new EventSource();
        var invokeCount = 0;
        source.Subscribe(() => invokeCount++);

        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Invoke());
    }

    [Fact]
    public void InvokeAfterDispose_ShouldThrow()
    {
        var source = new EventSource();
        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Invoke());
    }

    [Fact]
    public void SubscribeAfterDispose_ShouldThrow()
    {
        var source = new EventSource();
        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Subscribe(() => { }));
    }

    [Fact]
    public void Dispose_Twice_ShouldBeIdempotent()
    {
        var source = new EventSource();
        var ex = Record.Exception(() =>
        {
            source.Dispose();
            source.Dispose();
        });
        Assert.Null(ex);
    }

    [Fact]
    public void Invoke_WithNoSubscribers_ShouldNotThrow()
    {
        var source = new EventSource();
        var ex = Record.Exception(() => source.Invoke());
        Assert.Null(ex);
    }

    [Fact]
    public void SubscribeFromWithinHandler_ShouldNotDeadlock()
    {
        var source = new EventSource();
        Subscription? innerSub = null;

        source.Subscribe(() =>
        {
            innerSub = source.Subscribe(() => { });
        });

        var ex = Record.Exception(() => source.Invoke());
        Assert.Null(ex);
        Assert.NotNull(innerSub);
    }
}

public class EventSourceOfTTests
{
    [Fact]
    public void Invoke_ShouldPassValueToSubscriber()
    {
        var source = new EventSource<int>();
        var received = 0;
        source.Subscribe(v => received = v);

        source.Invoke(42);

        Assert.Equal(42, received);
    }

    [Fact]
    public void Subscribe_WithNullHandler_ShouldThrow()
    {
        var source = new EventSource<int>();
        Assert.Throws<ArgumentNullException>(() => source.Subscribe((Action<int>?)null!));
    }

    [Fact]
    public void MultipleSubscribers_ShouldEachReceiveValue()
    {
        var source = new EventSource<string>();
        var results = new List<string>();
        source.Subscribe(v => results.Add($"A:{v}"));
        source.Subscribe(v => results.Add($"B:{v}"));

        source.Invoke("hello");

        Assert.Equal(2, results.Count);
        Assert.Contains("A:hello", results);
        Assert.Contains("B:hello", results);
    }

    [Fact]
    public void Unsubscribe_ShouldStopReceiving()
    {
        var source = new EventSource<int>();
        var calls = new List<int>();
        var sub = source.Subscribe(v => calls.Add(v));

        source.Invoke(1);
        sub.Dispose();
        source.Invoke(2);

        Assert.Single(calls);
        Assert.Equal(1, calls[0]);
    }

    [Fact]
    public void Dispose_ShouldPreventInvocation()
    {
        var source = new EventSource<int>();
        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Invoke(1));
    }

    [Fact]
    public void SubscribeAfterDispose_ShouldThrow()
    {
        var source = new EventSource<int>();
        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Subscribe(v => { }));
    }
}

public class NotifierInterfaceDefaultMethodsTests
{
    [Fact]
    public void INotifier_SubscribeWithSubscription_ShouldWork()
    {
        var source = new EventSource();
        Subscription? captured = null;

        ((INotifier)source).Subscribe(sub => captured = sub);

        // Trigger the event so the self-referencing subscription runs
        source.Invoke();

        Assert.NotNull(captured);
    }

    [Fact]
    public void INotifierOfT_SubscribeWithoutValue_ShouldWork()
    {
        var source = new EventSource<int>();
        var invoked = false;

        ((INotifier)source).Subscribe(() => invoked = true);

        source.Invoke(42);

        Assert.True(invoked);
    }

    [Fact]
    public void INotifierOfT_SubscribeWithValueAndSubscription_ShouldWork()
    {
        var source = new EventSource<string>();
        string? capturedValue = null;
        Subscription? capturedSub = null;

        ((INotifier<string>)source).Subscribe((v, s) => { capturedValue = v; capturedSub = s; });

        source.Invoke("test");

        Assert.Equal("test", capturedValue);
        Assert.NotNull(capturedSub);
    }
}
