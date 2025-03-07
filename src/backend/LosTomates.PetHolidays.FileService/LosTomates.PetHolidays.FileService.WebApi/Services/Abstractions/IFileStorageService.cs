using LosTomates.PetHolidays.FileService.WebApi.Dto;

namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

public interface IFileStorageService
{
    Task<FileUploadResponse> UploadFileAsync(IFormFile file, string entityId, string collectionName);
    Task<FileDownloadResponse> DownloadFileAsync(string entityId, string collectionName);
    Task DeleteFileAsync(string entityId, string collectionName);
    Task<string> GetFileUrlAsync(string entityId, string collectionName);
}
