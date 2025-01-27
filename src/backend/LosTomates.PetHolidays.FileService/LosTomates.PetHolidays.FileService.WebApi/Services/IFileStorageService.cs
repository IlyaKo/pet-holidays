using LosTomates.PetHolidays.FileService.WebApi.Models;

namespace LosTomates.PetHolidays.FileService.WebApi.Services;
public interface IFileStorageService
{
    Task CreateBucket(string bucketName, CancellationToken cancellationToken = default);
    Task DeleteFile(string bucketName, string objectName, CancellationToken cancellationToken = default);
    Task<FileDownloadResponse> GetFileAsync(FileDownloadRequest request, CancellationToken cancellationToken = default);
    Task<FileUploadResponse> UploadFile(FileUploadRequest fileUploadRequest, CancellationToken cancellationToken = default);
}