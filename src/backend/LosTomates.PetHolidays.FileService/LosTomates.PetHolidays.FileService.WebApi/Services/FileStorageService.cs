using LosTomates.PetHolidays.FileService.WebApi.Models;
using Minio;
using Minio.DataModel.Args;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class FileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;

    public FileStorageService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task CreateBucket(string bucketName, CancellationToken cancellationToken = default)
    {
        var createBucketArgs = new MakeBucketArgs().WithBucket(bucketName);

        await _minioClient.MakeBucketAsync(createBucketArgs, cancellationToken);
    }

    public async Task<FileUploadResponse> UploadFile(FileUploadRequest fileUploadRequest, CancellationToken cancellationToken = default)
    {
        await using var ms = new MemoryStream(Convert.FromBase64String(fileUploadRequest.content));

        var fileName = $"{Guid.NewGuid():N}";

        ms.Position = 0;

        var createFileArgs = new PutObjectArgs()
            .WithBucket(fileUploadRequest.bucket)
            .WithStreamData(ms)
            .WithObjectSize(ms.Length)
            .WithObject(fileName)
            .WithContentType(!string.IsNullOrEmpty(fileUploadRequest.contentType)
                ? fileUploadRequest.contentType
                : "application/octet-stream");

        var response = await _minioClient.PutObjectAsync(createFileArgs, cancellationToken: cancellationToken);

        return new FileUploadResponse(response.ObjectName, fileUploadRequest.bucket);
    }


    public async Task<FileDownloadResponse> GetFileAsync(FileDownloadRequest request, CancellationToken cancellationToken = default)
    {
        var downloadLinkArgs = new PresignedGetObjectArgs()
            .WithBucket(request.bucketName)
            .WithObject(request.fileName)
            .WithExpiry(60 * 60);

        var downloadLinkRequest = await _minioClient.PresignedGetObjectAsync(downloadLinkArgs);

        return new FileDownloadResponse(downloadLinkRequest);
    }

    public async Task DeleteFile(string bucketName, string objectName, CancellationToken cancellationToken = default)
    {
        var removeBucketArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        await _minioClient.RemoveObjectAsync(removeBucketArgs, cancellationToken);
    }
}
