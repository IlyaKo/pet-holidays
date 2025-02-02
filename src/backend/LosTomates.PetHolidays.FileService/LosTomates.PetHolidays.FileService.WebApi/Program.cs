using LosTomates.PetHolidays.FileService.WebApi.Configuration;
using LosTomates.PetHolidays.FileService.WebApi.Dto;
using LosTomates.PetHolidays.FileService.WebApi.Extensions;
using LosTomates.PetHolidays.FileService.WebApi.Services;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Minio;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<MinioSettings>(builder.Configuration.GetSection("Minio"));

builder.Services.AddSingleton<IMinioClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<MinioSettings>>().Value;
    Console.WriteLine($"Using MinIO Endpoint: {options.Endpoint}");

    var endpoint = options.Endpoint?.Trim();

    if (string.IsNullOrWhiteSpace(endpoint) || !endpoint.StartsWith("http"))
    {
        throw new Exception($"Invalid MinIO endpoint: {endpoint}");
    }


    var minioClient = new MinioClient()
        .WithEndpoint(options.Endpoint)
        .WithCredentials(options.AccessKey, options.SecretKey);

    if (options.UseSSL)
        minioClient = minioClient.WithSSL();

    return minioClient.Build();
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/upload", async (IFileStorageService fileStorageService, IFormFile file, [FromForm] UploadFileDto request) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest("File is required.");

    if (string.IsNullOrWhiteSpace(request.BucketName) || string.IsNullOrWhiteSpace(request.EntityId) || string.IsNullOrWhiteSpace(request.CollectionName))
        return Results.BadRequest("Bucket name, entity ID, and collection name are required.");

    var fileUrl = await fileStorageService.UploadFileAsync(file, request.EntityId, request.BucketName, request.CollectionName);
    return Results.Ok(new { url = fileUrl });
})
.WithName("Upload file")
.DisableAntiforgery();


//app.MapGet("/download", async ([FromServices] IFileStorageService fileService,
//                               [FromQuery] string bucketName,
//                               [FromQuery] string entityId,
//                               [FromQuery] string collectionName) =>
//{
//    if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(entityId) || string.IsNullOrWhiteSpace(collectionName))
//        return Results.BadRequest("Bucket name, entity ID, and collection name are required.");

//    try
//    {
//        var fileStream = await fileService.DownloadFileAsync(bucketName, entityId, collectionName);
//        return Results.File(fileStream, "application/octet-stream", entityId);
//    }
//    catch (FileNotFoundException)
//    {
//        return Results.NotFound("File not found.");
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem($"Ошибка при скачивании файла: {ex.Message}");
//    }
//})
//.WithName("Download file")
//.Produces<FileStream>(StatusCodes.Status200OK)
//.Produces(StatusCodes.Status404NotFound)
//.Produces(StatusCodes.Status400BadRequest)
//.Produces(StatusCodes.Status500InternalServerError);


//app.MapDelete("/delete", async ([FromServices] IFileStorageService fileService,
//                                [FromQuery] string bucketName,
//                                [FromQuery] string entityId,
//                                [FromQuery] string collectionName) =>
//{
//    if (string.IsNullOrWhiteSpace(bucketName) || string.IsNullOrWhiteSpace(entityId) || string.IsNullOrWhiteSpace(collectionName))
//        return Results.BadRequest("Bucket name, entity ID, and collection name are required.");

//    try
//    {
//        await fileService.DeleteFileAsync(entityId, bucketName, collectionName);
//        return Results.NoContent();
//    }
//    catch (FileNotFoundException)
//    {
//        return Results.NotFound("File not found.");
//    }
//    catch (Exception ex)
//    {
//        return Results.Problem($"Ошибка при удалении файла: {ex.Message}");
//    }
//})
//.WithName("Delete file")
//.Produces(StatusCodes.Status204NoContent)
//.Produces(StatusCodes.Status404NotFound)
//.Produces(StatusCodes.Status400BadRequest)
//.Produces(StatusCodes.Status500InternalServerError);



app.Run();
