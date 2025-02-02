using LosTomates.PetHolidays.FileService.WebApi.Models;

namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

public interface IMetadataStorage
{
    Task InsertMetadataAsync(FileMetadata metadata, string collectionName);
    Task<FileMetadata?> FindFileByEntityAsync(string entityId, string collectionName);
    Task DeleteFileByEntityAsync(string entityId, string collectionName);
}
