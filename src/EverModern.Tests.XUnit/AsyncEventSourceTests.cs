using EverModern.Events;

namespace EverModern.Tests.XUnit;

public class AsyncEventSourceTests
{
    [Fact]
    public async Task InvokeAsync_ShouldCallSubscriber()
    {
        var source = new AsyncEventSource();
        var invoked = false;
        source.Subscribe(() => { invoked = true; return ValueTask.CompletedTask; });

        await source.InvokeAsync();

        Assert.True(invoked);
    }

    [Fact]
    public void Subscribe_WithNullHandler_ShouldThrow()
    {
        var source = new AsyncEventSource();
        Assert.Throws<ArgumentNullException>(() => source.Subscribe((Func<ValueTask>?)null!));
    }

    [Fact]
    public async Task Unsubscribe_ShouldStopReceivingNotifications()
    {
        var source = new AsyncEventSource();
        var invokeCount = 0;
        var sub = source.Subscribe(() => { invokeCount++; return ValueTask.CompletedTask; });

        await source.InvokeAsync();
        sub.Dispose();
        await source.InvokeAsync();

        Assert.Equal(1, invokeCount);
    }

    [Fact]
    public async Task MultipleSubscribers_ShouldAllBeInvoked()
    {
        var source = new AsyncEventSource();
        var count = 0;
        source.Subscribe(() => { count++; return ValueTask.CompletedTask; });
        source.Subscribe(() => { count++; return ValueTask.CompletedTask; });

        await source.InvokeAsync();

        Assert.Equal(2, count);
    }

    [Fact]
    public void Dispose_ShouldClearAllHandlers()
    {
        var source = new AsyncEventSource();
        source.Subscribe(() => { return ValueTask.CompletedTask; });

        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.InvokeAsync().AsTask().GetAwaiter().GetResult());
    }

    [Fact]
    public void SubscribeAfterDispose_ShouldThrow()
    {
        var source = new AsyncEventSource();
        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Subscribe(() => ValueTask.CompletedTask));
    }

    [Fact]
    public void Dispose_Twice_ShouldBeIdempotent()
    {
        var source = new AsyncEventSource();
        var ex = Record.Exception(() =>
        {
            source.Dispose();
            source.Dispose();
        });
        Assert.Null(ex);
    }

    [Fact]
    public async Task InvokeAsync_WithNoSubscribers_ShouldNotThrow()
    {
        var source = new AsyncEventSource();
        var ex = await Record.ExceptionAsync(() => source.InvokeAsync().AsTask());
        Assert.Null(ex);
    }

    [Fact]
    public async Task Handlers_ShouldExecuteSequentially()
    {
        var source = new AsyncEventSource();
        var executionOrder = new List<int>();

        source.Subscribe(async () =>
        {
            executionOrder.Add(1);
            await Task.Delay(10);
        });
        source.Subscribe(() =>
        {
            executionOrder.Add(2);
            return ValueTask.CompletedTask;
        });

        await source.InvokeAsync();

        Assert.Equal(2, executionOrder.Count);
        Assert.Equal(1, executionOrder[0]);
        Assert.Equal(2, executionOrder[1]);
    }

    [Fact]
    public async Task SubscribeFromWithinHandler_ShouldNotDeadlock()
    {
        var source = new AsyncEventSource();
        Subscription? innerSub = null;

        source.Subscribe(() =>
        {
            innerSub = source.Subscribe(() => ValueTask.CompletedTask);
            return ValueTask.CompletedTask;
        });

        var ex = await Record.ExceptionAsync(() => source.InvokeAsync().AsTask());
        Assert.Null(ex);
        Assert.NotNull(innerSub);
    }
}

public class AsyncEventSourceOfTTests
{
    [Fact]
    public async Task InvokeAsync_ShouldPassValueToSubscriber()
    {
        var source = new AsyncEventSource<int>();
        var received = 0;
        source.Subscribe(v => { received = v; return ValueTask.CompletedTask; });

        await source.InvokeAsync(42);

        Assert.Equal(42, received);
    }

    [Fact]
    public void Subscribe_WithNullHandler_ShouldThrow()
    {
        var source = new AsyncEventSource<int>();
        Assert.Throws<ArgumentNullException>(() => source.Subscribe((Func<int, ValueTask>?)null!));
    }

    [Fact]
    public async Task MultipleSubscribers_ShouldEachReceiveValue()
    {
        var source = new AsyncEventSource<string>();
        var results = new List<string>();
        source.Subscribe(async v => { results.Add($"A:{v}"); await Task.CompletedTask; });
        source.Subscribe(v => { results.Add($"B:{v}"); return ValueTask.CompletedTask; });

        await source.InvokeAsync("hello");

        Assert.Equal(2, results.Count);
        Assert.Contains("A:hello", results);
        Assert.Contains("B:hello", results);
    }

    [Fact]
    public async Task Unsubscribe_ShouldStopReceiving()
    {
        var source = new AsyncEventSource<int>();
        var calls = new List<int>();
        var sub = source.Subscribe(v => { calls.Add(v); return ValueTask.CompletedTask; });

        await source.InvokeAsync(1);
        sub.Dispose();
        await source.InvokeAsync(2);

        Assert.Single(calls);
        Assert.Equal(1, calls[0]);
    }

    [Fact]
    public async Task InvokeAfterDispose_ShouldThrow()
    {
        var source = new AsyncEventSource<string>();
        source.Dispose();

        await Assert.ThrowsAsync<ObjectDisposedException>(async () => await source.InvokeAsync("x"));
    }

    [Fact]
    public void SubscribeAfterDispose_ShouldThrow()
    {
        var source = new AsyncEventSource<int>();
        source.Dispose();

        Assert.Throws<ObjectDisposedException>(() => source.Subscribe(v => ValueTask.CompletedTask));
    }
}

public class AsyncNotifierInterfaceDefaultMethodsTests
{
    [Fact]
    public async Task IAsyncNotifier_SubscribeWithSubscription_ShouldWork()
    {
        var source = new AsyncEventSource();
        Subscription? captured = null;

        ((IAsyncNotifier)source).Subscribe(sub => { captured = sub; return ValueTask.CompletedTask; });

        await source.InvokeAsync();

        Assert.NotNull(captured);
    }

    [Fact]
    public async Task IAsyncNotifierOfT_SubscribeWithoutValue_ShouldWork()
    {
        var source = new AsyncEventSource<int>();
        var invoked = false;

        ((IAsyncNotifier)source).Subscribe(() => { invoked = true; return ValueTask.CompletedTask; });

        await source.InvokeAsync(42);

        Assert.True(invoked);
    }

    [Fact]
    public async Task IAsyncNotifierOfT_SubscribeWithValueAndSubscription_ShouldWork()
    {
        var source = new AsyncEventSource<string>();
        string? capturedValue = null;
        Subscription? capturedSub = null;

        ((IAsyncNotifier<string>)source).Subscribe(
            (v, s) => { capturedValue = v; capturedSub = s; return ValueTask.CompletedTask; }
        );

        await source.InvokeAsync("test");

        Assert.Equal("test", capturedValue);
        Assert.NotNull(capturedSub);
    }

    [Fact]
    public async Task IAsyncNotifierOfT_DefaultSubscribe_ShouldBridgeToTypedSubscribe()
    {
        var source = new AsyncEventSource<double>();
        var invoked = false;

        ((IAsyncNotifier)source).Subscribe(() => { invoked = true; return ValueTask.CompletedTask; });

        await source.InvokeAsync(3.14);

        Assert.True(invoked);
    }
}
