using EverModern.Events;

namespace EverModern.Tests.XUnit;

public class ObservedValueTests
{
    [Fact]
    public void Constructor_ShouldSetInitialValue()
    {
        // Arrange & Act
        var ov = new ObservedValue<int>(42);

        // Assert
        Assert.Equal(42, ov.Value);
    }

    [Fact]
    public void Value_ShouldReturnLatest()
    {
        // Arrange
        var ov = new ObservedValue<string>("hello");

        // Act
        ov.Change("world");

        // Assert
        Assert.Equal("world", ov.Value);
    }

    [Fact]
    public void Change_ShouldNotifySubscribersWithNewValue()
    {
        // Arrange
        var ov = new ObservedValue<int>(0);
        var received = 0;
        ov.Subscribe(v => received = v);

        // Act
        ov.Change(99);

        // Assert
        Assert.Equal(99, received);
    }

    [Fact]
    public void Change_ShouldNotifyBeforeUpdatingValue()
    {
        // Arrange
        var ov = new ObservedValue<int>(10);
        var valueAtNotification = 0;
        ov.Subscribe(v => valueAtNotification = ov.Value);

        // Act
        ov.Change(20);

        // Assert
        // ObservedValue calls base.Invoke (which fires handlers) BEFORE updating _value,
        // so during notification Value is still the old one
        Assert.Equal(10, valueAtNotification);
        Assert.Equal(20, ov.Value);
    }

    [Fact]
    public void MultipleSubscribers_ShouldAllBeNotifiedOnChange()
    {
        // Arrange
        var ov = new ObservedValue<int>(0);
        var results = new List<int>();
        ov.Subscribe(v => results.Add(v));
        ov.Subscribe(v => results.Add(v));

        // Act
        ov.Change(7);

        // Assert
        Assert.Equal(2, results.Count);
        Assert.All(results, v => Assert.Equal(7, v));
    }

    [Fact]
    public void Unsubscribe_ShouldStopReceivingChanges()
    {
        // Arrange
        var ov = new ObservedValue<int>(0);
        var calls = new List<int>();
        var sub = ov.Subscribe(v => calls.Add(v));

        // Act
        ov.Change(1);
        sub.Dispose();
        ov.Change(2);

        // Assert
        Assert.Single(calls);
    }

    [Fact]
    public void Subscribe_WithNullHandler_ShouldThrow()
    {
        // Arrange
        var ov = new ObservedValue<int>(0);

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => ov.Subscribe((Action<int>?)null!));
    }

    [Fact]
    public void SupportsReferenceTypes()
    {
        // Arrange
        var ov = new ObservedValue<string?>(null);

        // Act & Assert
        Assert.Null(ov.Value);
        ov.Change("hello");
        Assert.Equal("hello", ov.Value);
    }

    [Fact]
    public void Implements_IValueNotifier()
    {
        // Arrange
        var ov = new ObservedValue<int>(42);

        // Act & Assert
        IValueNotifier<int> notifier = ov;
        Assert.Equal(42, notifier.Value);
    }

    [Fact]
    public void Dispose_ShouldNotThrow()
    {
        // Arrange
        var ov = new ObservedValue<int>(0);

        // Act & Assert
        var ex = Record.Exception(() => ov.Dispose());
        Assert.Null(ex);
    }
}
