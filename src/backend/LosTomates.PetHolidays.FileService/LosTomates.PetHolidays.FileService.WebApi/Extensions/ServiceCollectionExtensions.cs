using LosTomates.PetHolidays.FileService.WebApi.Services;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;
using Microsoft.AspNetCore.Connections;
using RabbitMQ.Client;

namespace LosTomates.PetHolidays.FileService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddSingleton<IFileStorage, MinioFileStorage>();
        services.AddSingleton<IMetadataStorage, MongoMetadataStorage>();
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }

    internal static IServiceCollection AddRabbitMQ(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("RabbitMq");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ApplicationException("An environment variable named ConnectionStrings__RabbitMQ is not set");

        services.AddSingleton(new ConnectionFactory { HostName = connectionString });

        return services;
    }
}
