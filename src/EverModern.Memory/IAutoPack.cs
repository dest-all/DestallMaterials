namespace EverModern.Memory;

/// <summary>
/// When applied to a partial class, the class gets the methods to pack and unpack instances of the specified types.
/// </summary>
/// <typeparam name="TAllowed">A tuple of types to pack (or a single type)</typeparam>
public interface IAutoPack<TAllowed>
{

}
