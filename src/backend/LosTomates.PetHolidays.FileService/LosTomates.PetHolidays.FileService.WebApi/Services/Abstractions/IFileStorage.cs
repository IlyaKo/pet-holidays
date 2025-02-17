namespace LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
public interface IFileStorage
{
    Task<string> UploadFileAsync(string objectName, Stream stream, string bucketName);
    Task<Stream> DownloadFileAsync(string objectName, string bucketName);
    Task DeleteFilesAsync(List<string> objectNames, string bucketName);
}