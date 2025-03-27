using LosTomates.PetHolidays.FileService.WebApi.Configuration;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class MinioFileStorage : IFileStorage
{
    private readonly IMinioClient _minioClient;
    private readonly string _baseUrl;
    private readonly string _publicUrl;

    public MinioFileStorage(IOptions<MinioSettings> options, IMinioClient minioClient)
    {
        _minioClient = minioClient ?? throw new ArgumentNullException(nameof(minioClient));
        _baseUrl = options.Value.Endpoint ?? throw new ArgumentException("BaseUrl must not be null", nameof(options));
        _publicUrl = options.Value.PublicUrl ?? throw new ArgumentException("PublicUrl must not be null", nameof(options));
    }

    public async Task<string> UploadFileAsync(string objectName, Stream stream, string bucketName)
    {
        var found = await _minioClient.BucketExistsAsync(new BucketExistsArgs().WithBucket(bucketName));

        if (!found)
        {
            await _minioClient.MakeBucketAsync(new MakeBucketArgs().WithBucket(bucketName));
            await SetPublicBucketPolicyAsync(bucketName);
        }

        await _minioClient.PutObjectAsync(
            new PutObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
        );

        return $"{_publicUrl}/{bucketName}/{objectName}";
    }

    public async Task<Stream> DownloadFileAsync(string objectName, string bucketName)
    {
        var memoryStream = new MemoryStream();

        await _minioClient.GetObjectAsync(
            new GetObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName)
                .WithCallbackStream(stream => stream.CopyTo(memoryStream))
        );
        memoryStream.Position = 0;
        return memoryStream;
    }

    public async Task<string> GetFileUrlAsync(string objectName, string bucketName)
    {
        var statArgs = new StatObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);
        await _minioClient.StatObjectAsync(statArgs);

        return $"{_publicUrl}/{bucketName}/{objectName}";
    }

    public async Task DeleteFilesAsync(List<string> objectNames, string bucketName)
    {
        var deleteTasks = objectNames.Select(objectName =>
        {
            var removeObjectArgs = new RemoveObjectArgs()
                .WithBucket(bucketName)
                .WithObject(objectName);

            return _minioClient.RemoveObjectAsync(removeObjectArgs);
        });
        await Task.WhenAll(deleteTasks);
    }

    private async Task SetPublicBucketPolicyAsync(string bucketName)
    {
        var policyJson = $@"
        {{
          ""Version"": ""2012-10-17"",
          ""Statement"": [
            {{
              ""Effect"": ""Allow"",
              ""Principal"": ""*"",
              ""Action"": [
                ""s3:GetObject""
              ],
              ""Resource"": [
                ""arn:aws:s3:::{bucketName}/*""
              ]
            }}
          ]
        }}";

        await _minioClient.SetPolicyAsync(new SetPolicyArgs()
            .WithBucket(bucketName)
            .WithPolicy(policyJson));
    }
}
