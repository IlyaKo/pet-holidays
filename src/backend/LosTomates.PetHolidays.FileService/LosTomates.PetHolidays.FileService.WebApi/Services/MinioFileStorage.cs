using LosTomates.PetHolidays.FileService.WebApi.Configuration;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.Extensions.Options;
using Minio;
using Minio.DataModel.Args;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;

public class MinioFileStorage : IFileStorage
{
    private readonly IMinioClient _minioClient;

    public MinioFileStorage(IOptions<MinioSettings> options, IMinioClient minioClient)
    {
        _minioClient = minioClient;
    }

    public async Task<string> UploadFileAsync(string bucketName, string objectName, Stream stream)
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

        return $"{_minioClient.Config.Endpoint}/{bucketName}/{objectName}";
    }


    public async Task<Stream> DownloadFileAsync(string bucketName, string objectName)
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

    public async Task DeleteFileAsync(string bucketName, string objectName)
    {
        var removeBucketArgs = new RemoveObjectArgs()
            .WithBucket(bucketName)
            .WithObject(objectName);

        await _minioClient.RemoveObjectAsync(removeBucketArgs);
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
