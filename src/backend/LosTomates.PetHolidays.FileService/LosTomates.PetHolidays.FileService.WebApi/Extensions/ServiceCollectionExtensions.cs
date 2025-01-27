using LosTomates.PetHolidays.FileService.WebApi.Services;

namespace LosTomates.PetHolidays.FileService.WebApi.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }
}
