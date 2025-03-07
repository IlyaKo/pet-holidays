using Microsoft.AspNetCore.Http;

namespace LosTomates.PetHolidays.Core.Core.FileService;
public interface IFileServiceClient
{
    Task<string> UploadFileAsync(IFormFile file, string entityId, string collectionName);
    Task<string> GetFileUrlAsync(string entityId, string collectionName);
    Task DeleteFileAsync(string entityId, string collectionName);
}
