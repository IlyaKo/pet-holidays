namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
public interface IFileStorage
{
    Task<string> UploadFileAsync(string bucketName, string objectName, Stream stream);
    Task<Stream> DownloadFileAsync(string bucketName, string objectName);
    Task DeleteFileAsync(string bucketName, string objectName);
}