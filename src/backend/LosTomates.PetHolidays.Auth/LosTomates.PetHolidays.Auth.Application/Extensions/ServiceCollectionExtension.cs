using Mapster;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace LosTomates.PetHolidays.Auth.Application.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton(config);
        return services;
    }
}

