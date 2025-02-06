using LosTomates.PetHolidays.FileService.WebApi.Dto;

namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

public interface IFileStorageService
{
    Task<string> UploadFileAsync(IFormFile file, string entityId, string bucketName, string collectionName);
    Task<FileResponse> DownloadFileAsync(string entityId, string bucketName, string collectionName);
    Task DeleteFileAsync(string entityId, string bucketName, string collectionName);
}
