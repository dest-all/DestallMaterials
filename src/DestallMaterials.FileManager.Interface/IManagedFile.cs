namespace DestallMaterials.FileManager.Interface
{
    public interface IManagedFile
    {
        float Size { get; }
        string Name { get; }
        Type ManagerType { get; }
    }
}