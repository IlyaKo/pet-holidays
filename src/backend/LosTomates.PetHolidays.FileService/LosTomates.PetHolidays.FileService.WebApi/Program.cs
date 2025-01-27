using LosTomates.PetHolidays.FileService.WebApi.Extensions;
using LosTomates.PetHolidays.FileService.WebApi.Models;
using LosTomates.PetHolidays.FileService.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Minio;

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


app.MapPost("/buckets/{bucketName}", async (string bucketName, IFileStorageService fileService, CancellationToken ct) =>
{
    await fileService.CreateBucket(bucketName, ct);
    return Results.Ok($"Bucket '{bucketName}' created.");
})
.WithName("Create bucket");

app.MapPost("/files/upload", async (FileUploadRequest request, IFileStorageService fileService, CancellationToken ct) =>
{
    var result = await fileService.UploadFile(request, ct);
    return Results.Ok(result);
})
.WithName("Upload file");


//app.MapPost("/files/upload-file", async (
//    [FromForm] IFormFile file,
//    [FromForm] string bucket,
//    [FromServices] IFileStorageService fileService) =>
//{

//    if (file is null || file.Length == 0)
//        return Results.BadRequest();

//    using var ms = new MemoryStream();
//    await file.CopyToAsync(ms);

//    ms.Position = 0;

//    var base64String = Convert.ToBase64String(ms.ToArray());

//    var requestModel = new FileUploadRequest(
//        base64String,
//        file.ContentType,
//        bucket
//    );

//    var result = await fileService.UploadFile(requestModel);
//    return Results.Ok();
//});


app.MapPost("/files/download", async (FileDownloadRequest request, IFileStorageService fileService, CancellationToken ct) =>
{
    var result = await fileService.GetFileAsync(request, ct);
    return Results.Ok(result);
})
.WithName("Get download link");

app.MapDelete("/files/{bucketName}/{objectName}", async (string bucketName, string objectName, IFileStorageService fileService, CancellationToken ct) =>
{
    await fileService.DeleteFile(bucketName, objectName, ct);
    return Results.NoContent();
})
.WithName("Delete file");



app.Run();


