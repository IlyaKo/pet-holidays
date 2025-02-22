using LosTomates.PetHolidays.FileService.WebApi.Dto;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace LosTomates.PetHolidays.FileService.WebApi.Endpoints;

public static class FileServiceEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/files")
                         .WithTags("File service")
                         .WithOpenApi();

        mapGroup.MapPost("/upload/{collectionName}/{entityId}", async (
            string entityId,
            string collectionName,
            IFormFile file,
            IFileStorageService fileStorageService) =>
        {
            if (file == null || file.Length == 0)
                return Results.BadRequest("File is required.");

            if (string.IsNullOrWhiteSpace(entityId) || string.IsNullOrWhiteSpace(collectionName))
                return Results.BadRequest("Сollection name, entity ID are required.");

            var allowedExtensions = new HashSet<string> { ".jpg", ".jpeg", ".png" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();

            if (!allowedExtensions.Contains(fileExtension))
                return Results.BadRequest("Unsupported file format.");

            try
            {
                var uploadFileResponse = await fileStorageService.UploadFileAsync(file, entityId, collectionName);
                return Results.Ok(uploadFileResponse);
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithSummary("Upload file")
        .DisableAntiforgery();

        mapGroup.MapGet("/download/{collectionName}/{entityId}", async (
            string entityId,
            string collectionName,
            IFileStorageService fileStorageService) =>
        {
            if (string.IsNullOrWhiteSpace(entityId) || string.IsNullOrWhiteSpace(collectionName))
                return Results.BadRequest("Сollection name, entity ID are required.");

            try
            {
                var fileResponse = await fileStorageService.DownloadFileAsync(entityId, collectionName);

                return Results.File(fileResponse.FileStream, fileResponse.ContentType, fileResponse.FileName, enableRangeProcessing: true);
            }
            catch (FileNotFoundException)
            {
                return Results.NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithSummary("Download file");

        mapGroup.MapGet("/{collectionName}/{entityId}/url", async (
            string entityId,
            string collectionName,
            IFileStorageService fileStorageService) =>
        {
            if (string.IsNullOrWhiteSpace(entityId) || string.IsNullOrWhiteSpace(collectionName))
                return Results.BadRequest("Сollection name, entity ID are required.");

            try
            {
                var url = await fileStorageService.GetFileUrlAsync(entityId, collectionName);
                return Results.Ok(new { Url = url });
            }
            catch (FileNotFoundException)
            {
                return Results.NotFound("File not found");
            }
        })
        .WithSummary("Get file url")
        .WithOpenApi();

        mapGroup.MapDelete("/{collectionName}/{entityId}", async (
            string entityId,
            string collectionName,
            IFileStorageService fileStorageService) =>
        {

            if (string.IsNullOrWhiteSpace(entityId) || string.IsNullOrWhiteSpace(collectionName))
                return Results.BadRequest("Сollection name, entity ID are required.");

            try
            {
                await fileStorageService.DeleteFileAsync(entityId, collectionName);
                return Results.Ok();
            }
            catch (FileNotFoundException)
            {
                return Results.NotFound("File not found.");
            }
            catch (Exception ex)
            {
                return Results.BadRequest(ex.Message);
            }
        })
        .WithSummary("Delete file");
    }
}
