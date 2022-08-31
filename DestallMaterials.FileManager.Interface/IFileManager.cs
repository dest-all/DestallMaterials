namespace DestallMaterials.FileManager.Interface
{
    public interface IFileManager
    {
        Task<IManagedFile> UploadFileAsync(string fileLocation);
        Task<IEnumerable<IManagedFile>> GetAllFilesAsync();
        Task<FileInfo> DownloadFileAsync(IManagedFile managedFile);
    }
}