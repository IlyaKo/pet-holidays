namespace LosTomates.PetHolidays.FileService.WebApi.Services;
public interface IFileStorageService
{
    Task<string> UploadFileAsync(string bucketName, string fileName, Stream stream, CancellationToken cancellationToken = default);
    Task<Stream> DownloadFileAsync(string bucketName, string fileName, CancellationToken cancellationToken = default);
    Task CreateBucket(string bucketName, CancellationToken cancellationToken = default);
    Task DeleteFile(string bucketName, string fileName, CancellationToken cancellationToken = default);


}