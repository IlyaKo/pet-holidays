using LosTomates.PetHolidays.FileService.WebApi.Dto;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LosTomates.PetHolidays.FileService.WebApi.Endpoints;

public static class FileServiceEndpoints
{
    public static void Map(WebApplication app)
    {
        app.MapPost("/upload", async (IFileStorageService fileStorageService, IFormFile file, [FromForm] UploadFileDto request) =>
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("File is required.");

            if (string.IsNullOrWhiteSpace(request.EntityId) || string.IsNullOrWhiteSpace(request.CollectionName))
                return Results.BadRequest("Bucket name, entity ID, and collection name are required.");

            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return Results.BadRequest("Unsupported file format.");

            try
            {
                var fileUrl = await fileStorageService.UploadFileAsync(file, request.EntityId, request.CollectionName);
                return Results.Ok(new { url = fileUrl });
            }
            catch (Exception ex)
            {
                return Results.Problem($"Ошибка при загрузке файла: {ex.Message}");
            }
        })
        .WithName("Upload file")
        .DisableAntiforgery();

        app.MapDelete("/delete", async ([FromServices] IFileStorageService fileService, [AsParameters] UploadFileDto request) =>
        {

            if (string.IsNullOrWhiteSpace(request.EntityId) || string.IsNullOrWhiteSpace(request.CollectionName))
                return Results.BadRequest("Bucket name, entity ID, and collection name are required.");

            try
            {
                await fileService.DeleteFileAsync(request.EntityId,  request.CollectionName);
                return Results.Ok();
            }
            catch (FileNotFoundException)
            {
                return Results.NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return Results.Problem($"Ошибка при удалении файла: {ex.Message}");
            }
        })
        .WithName("Delete file");

        app.MapGet("/download", async ([FromServices] IFileStorageService fileService, [AsParameters] UploadFileDto request) =>
        {
            if (string.IsNullOrWhiteSpace(request.EntityId) || string.IsNullOrWhiteSpace(request.CollectionName))
                return Results.BadRequest("Bucket name, entity ID, and collection name are required.");

            try
            {
                var fileResponse = await fileService.DownloadFileAsync(request.EntityId, request.CollectionName);

                return Results.File(fileResponse.FileStream, fileResponse.ContentType, fileResponse.FileName, enableRangeProcessing: true);
            }
            catch (FileNotFoundException)
            {
                return Results.NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return Results.Problem($"Ошибка при скачивании файла: {ex.Message}");
            }
        })
        .WithName("Download file");
    }
}
