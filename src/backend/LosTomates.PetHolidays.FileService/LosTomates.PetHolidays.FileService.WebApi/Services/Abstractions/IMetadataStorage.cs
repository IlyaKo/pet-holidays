using LosTomates.PetHolidays.FileService.WebApi.Models;

namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

public interface IMetadataStorage
{
    Task InsertMetadataAsync(FileMetadata metadata, string collectionName);
    Task<List<FileMetadata>> FindFilesByEntityIdAsync(string entityId, string collectionName);
    Task DeleteFileByEntitiesIdAsync(List<string> entitiesId, string collectionName);
}
