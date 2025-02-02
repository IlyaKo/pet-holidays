using LosTomates.PetHolidays.FileService.WebApi.Models;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IFileStorage _fileStorage;
    private readonly IMetadataStorage _metadataStorage; 

    public FileStorageService(IFileStorage fileStorageService, IMetadataStorage metadataStorageService)
    {
        _fileStorage = fileStorageService;
        _metadataStorage = metadataStorageService;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string entityId, string bucketName,  string collectionName)
    {
        var objectName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        string url;

        using (var stream = file.OpenReadStream())
        {
            url = await _fileStorage.UploadFileAsync(bucketName, objectName, stream);
        }

        var metadata = new FileMetadata
        {
            Url = url,
            BucketName = bucketName,
            EntityId = entityId
        };


        await _metadataStorage.InsertMetadataAsync(metadata, collectionName);

        return url;

    }

    public async Task<Stream> DownloadFileAsync(string bucketName, string entityId, string collectionName)
    {
        var metadata = await _metadataStorage.FindFileByEntityAsync(entityId, collectionName);
        if (metadata == null)
            throw new FileNotFoundException("File not found.");

        var objectName = metadata.Url.Split('/').Last();

        return await _fileStorage.DownloadFileAsync(bucketName, objectName);
    }

    public async Task DeleteFileAsync(string entityId, string bucketName, string collectionName)
    {
        var metadata = await _metadataStorage.FindFileByEntityAsync(entityId, collectionName);
        if (metadata == null)
            throw new FileNotFoundException("File not found.");

        var objectName = metadata.Url;

        await _fileStorage.DeleteFileAsync(bucketName, objectName);
        await _metadataStorage.DeleteFileByEntityAsync(metadata.Id, collectionName); 
    }
}
