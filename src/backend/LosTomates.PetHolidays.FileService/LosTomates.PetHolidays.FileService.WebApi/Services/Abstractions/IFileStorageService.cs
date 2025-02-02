namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string entityId, string bucketName, string collectionName);
    Task<Stream> DownloadFileAsync(string bucketName, string entityId, string collectionName);
    Task DeleteFileAsync(string entityId, string bucketName, string collectionName);
}
