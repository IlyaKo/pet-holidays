using LosTomates.PetHolidays.FileService.WebApi.Extensions;
using LosTomates.PetHolidays.FileService.WebApi.Models;
using LosTomates.PetHolidays.FileService.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMinio(client =>
{
    client.WithCredentials("minio", "minio")
        .WithEndpoint("localhost:9000")
        .WithSSL(false);
});

builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/files/upload", async (IFormFile file, IFileStorageService fileService, CancellationToken ct) =>
{
    if (file == null || file.Length == 0)
        return Results.BadRequest();

    using var stream = file.OpenReadStream();
    await fileService.UploadFileAsync("test-bucket", file.FileName, stream, ct);
    return Results.Ok("File uploaded successfully.");
})
.WithName("Upload file");


app.MapPost("/files/download", async (string bucketName, string fileName, IFileStorageService fileService, CancellationToken ct) =>
{
    var result = await fileService.DownloadFileAsync(bucketName, fileName);
    return Results.Ok(result);
})
.WithName("Download file");

app.MapPost("/buckets/{bucketName}", async (string bucketName, IFileStorageService fileService, CancellationToken ct) =>
{
    await fileService.CreateBucket(bucketName, ct);
    return Results.Ok($"Bucket '{bucketName}' created.");
})
.WithName("Create bucket");

app.MapDelete("/files/{bucketName}/{objectName}", async (string bucketName, string objectName, IFileStorageService fileService, CancellationToken ct) =>
{
    await fileService.DeleteFile(bucketName, objectName, ct);
    return Results.NoContent();
})
.WithName("Delete file");



app.Run();


