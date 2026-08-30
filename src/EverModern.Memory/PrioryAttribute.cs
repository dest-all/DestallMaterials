namespace EverModern.Memory;

public class PrioryAttribute : Attribute
{
    public int Priority { get; }
    public PrioryAttribute(int priority)
    {
        Priority = priority;
    }
}
