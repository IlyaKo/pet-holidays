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

    public async Task<string> UploadFileAsync(IFormFile file, string entityId, string bucketName, string collectionName)
    {
        var objectName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
        var contentType = file.ContentType ?? "application/octet-stream";

        string url;

        using (var stream = file.OpenReadStream())
            url = await _fileStorage.UploadFileAsync(objectName, stream, bucketName);

        var metadata = new FileMetadata
        {
            Url = url,
            BucketName = bucketName,
            EntityId = entityId,
            OriginalFileName = file.FileName,
            FileExtension = Path.GetExtension(file.FileName),
            FileSize = file.Length,
            ContentType = contentType
        };

        await _metadataStorage.InsertMetadataAsync(metadata, collectionName);

        return url;
    }

    public async Task<FileResponse> DownloadFileAsync(string entityId, string bucketName, string collectionName)
    {
        var files = await _metadataStorage.FindFilesByEntityIdAsync(entityId, collectionName);
        if (!files.Any())
            throw new FileNotFoundException("File not found.");

        var file = files
            .OrderBy(f => f.UploadedAt)
            .Last();

        var objectName = Path.GetFileName(new Uri(file.Url).AbsolutePath);

        var fileStream = await _fileStorage.DownloadFileAsync(objectName, bucketName);

        return new FileResponse()
        {
            FileStream = fileStream,
            FileName = file.OriginalFileName,
            ContentType = file.ContentType,
            FileSize = file.FileSize
        };
    }

    public async Task DeleteFileAsync(string entityId, string bucketName, string collectionName)
    {
        var filesMetadata = await _metadataStorage.FindFilesByEntityIdAsync(entityId, collectionName);
        if (!filesMetadata.Any())
            throw new FileNotFoundException("File not found.");

        var objectNames = filesMetadata
            .Select(x => Path.GetFileName(new Uri(x.Url).AbsolutePath))
            .ToList();

        await _fileStorage.DeleteFilesAsync(objectNames, bucketName);

        await _metadataStorage.DeleteFileByEntitiesIdAsync(filesMetadata.Select(x => x.EntityId).ToList(), collectionName);
    }
}
