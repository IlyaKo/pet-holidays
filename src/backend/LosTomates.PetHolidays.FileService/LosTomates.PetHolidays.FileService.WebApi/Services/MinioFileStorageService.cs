using Minio;
using Minio.DataModel.Args;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class MinioFileStorageService : IFileStorageService
{
    private readonly IMinioClient _minioClient;

    public MinioFileStorageService(IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadFileAsync(string bucketName, string fileName, Stream stream, CancellationToken cancellationToken = default)
    {
        var bucketExistsArgs = new BucketExistsArgs()
            .WithBucket(bucketName);

        var bucketExists = await _minioClient.BucketExistsAsync(bucketExistsArgs, cancellationToken);
        if (!bucketExists)
        {
            var makeBucketArgs = new MakeBucketArgs()
                    .WithBucket(bucketName);

            await _minioClient.MakeBucketAsync(makeBucketArgs, cancellationToken);
        }

        var createFileArgs = new PutObjectArgs()
            .WithBucket(bucketName)
            .WithStreamData(stream)
            .WithObjectSize(stream.Length)
            .WithObject(fileName)
            .WithContentType("application/octet-stream");

        await _minioClient.PutObjectAsync(createFileArgs, cancellationToken);

        var url = $"{_minioClient.Config.Endpoint}/{bucketName}/{fileName}";
        return url;
    }


    public async Task<Stream> DownloadFileAsync(string bucketName, string fileName, CancellationToken cancellationToken = default)
    {
        var memoryStream = new MemoryStream();

        var getObjectArgs = new GetObjectArgs()
            .WithBucket(bucketName)
            .WithObject(fileName)
            .WithCallbackStream(stream =>
            {
                stream.CopyTo(memoryStream);
            });

        await _minioClient.GetObjectAsync(getObjectArgs, cancellationToken);

        return memoryStream;
    }

    public async Task CreateBucket(string bucketName, CancellationToken cancellationToken = default)
    {
        var createBucketArgs = new MakeBucketArgs()
            .WithBucket(bucketName);

        await _minioClient.MakeBucketAsync(createBucketArgs, cancellationToken);
    }

    public async Task DeleteFile(string bucketName, string objectName, CancellationToken cancellationToken = default)
    {
        var removeBucketArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        await _minioClient.RemoveObjectAsync(removeBucketArgs, cancellationToken);
    }
}
