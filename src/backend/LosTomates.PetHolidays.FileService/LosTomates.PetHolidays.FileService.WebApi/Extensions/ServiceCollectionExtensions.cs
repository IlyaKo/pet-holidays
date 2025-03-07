using LosTomates.PetHolidays.FileService.WebApi.Services;
using LosTomates.PetHolidays.FileService.WebApi.Services.Abstractions;

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
}
