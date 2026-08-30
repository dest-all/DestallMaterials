namespace EverModern.Events;

public class ObservedValue<TValue>(
    TValue value
) : EventSource<TValue>
{
    TValue _value = value;
    public TValue Value => _value;

    public override void Invoke(TValue newValue)
    {
        base.Invoke(newValue);
        _value = newValue;
    }
}
