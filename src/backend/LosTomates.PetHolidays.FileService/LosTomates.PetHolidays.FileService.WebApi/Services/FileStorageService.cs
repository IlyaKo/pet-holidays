using LosTomates.PetHolidays.FileService.WebApi.Dto;
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

    public async Task<FileUploadResponse> UploadFileAsync(IFormFile file, string entityId, string collectionName)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("No file uploaded");

        var objectName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var contentType = file.ContentType ?? "application/octet-stream";

        using var stream = file.OpenReadStream();
        var url = await _fileStorage.UploadFileAsync(objectName, stream, collectionName);

        var metadata = new FileMetadata
        {
            ObjectName = objectName,
            BucketName = collectionName,
            EntityId = entityId,
            OriginalFileName = file.FileName,
            FileExtension = Path.GetExtension(file.FileName),
            FileSize = file.Length,
            ContentType = contentType
        };

        await _metadataStorage.InsertMetadataAsync(metadata, collectionName);

        return new FileUploadResponse { Url = url };
    }

    public async Task<FileDownloadResponse> DownloadFileAsync(string entityId, string collectionName)
    {
        var files = await _metadataStorage.FindFilesByEntityIdAsync(entityId, collectionName);
        if (!files.Any())
            throw new FileNotFoundException("File not found");

        var file = files
            .OrderBy(f => f.UploadedAt)
            .Last();

        var fileStream = await _fileStorage.DownloadFileAsync(file.ObjectName, collectionName);

        return new FileDownloadResponse()
        {
            FileStream = fileStream,
            FileName = file.OriginalFileName,
            ContentType = file.ContentType,
            FileSize = file.FileSize
        };
    }

    public async Task DeleteFileAsync(string entityId, string collectionName)
    {
        var filesMetadata = await _metadataStorage.FindFilesByEntityIdAsync(entityId, collectionName);
        if (!filesMetadata.Any())
            throw new FileNotFoundException("File not found");

        var objectNames = filesMetadata.Select(x => x.ObjectName).ToList();

        await _fileStorage.DeleteFilesAsync(objectNames, collectionName);

        await _metadataStorage.DeleteFileByEntitiesIdAsync(filesMetadata.Select(x => x.EntityId).ToList(), collectionName);
    }

    public async Task<string> GetFileUrlAsync(string entityId, string collectionName)
    {
        var files = await _metadataStorage.FindFilesByEntityIdAsync(entityId, collectionName);
        if (!files.Any())
            throw new FileNotFoundException("File not found");

        var file = files.OrderBy(f => f.UploadedAt).Last();
        
        return await _fileStorage.GetFileUrlAsync(file.ObjectName, collectionName);
    }
}
