using LosTomates.PetHolidays.FileService.WebApi.Configuration;
using LosTomates.PetHolidays.FileService.WebApi.Endpoints;
using LosTomates.PetHolidays.FileService.WebApi.Extensions;
using Microsoft.Extensions.Options;
using Minio;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();

builder.Services.Configure<MongoDbSettings>(builder.Configuration.GetSection("MongoDB"));
builder.Services.Configure<MinioSettings>(builder.Configuration.GetSection("Minio"));

builder.Services.AddSingleton(sp =>
{
    var options = sp.GetRequiredService<IOptions<MinioSettings>>().Value;

    var endpointUri = new Uri(options.BaseUrl);
    var host = endpointUri.Host;
    var port = endpointUri.Port;

    var minioClient = new MinioClient()
        .WithEndpoint(host, port)
        .WithCredentials(options.AccessKey, options.SecretKey);

    return minioClient.Build();
});

builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

FileServiceEndpoints.Map(app);

app.Run();
