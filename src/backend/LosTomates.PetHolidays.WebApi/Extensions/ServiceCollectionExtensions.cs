using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.Rooms;
using LosTomates.PetHolidays.Application.RoomTypes;
using LosTomates.PetHolidays.Application.Users;
using LosTomates.PetHolidays.DataAccess;
using LosTomates.PetHolidays.DataAccess.DataSeed;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.WebApi.Extensions;

internal static class ServiceCollectionExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {

        });

        return services;
    }

    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IRoomService, RoomService>();
        services.AddScoped<IRoomTypeService, RoomTypeService>();

        return services;
    }

    internal static IServiceCollection AddDatabaseContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CoreDb");
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ApplicationException("An environment variable named ConnectionStrings__CoreDb is not set");

        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
        services.AddTransient<SeedService>();

        return services;
    }

    internal static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(HotelEditDtoValidator).Assembly);

        return services;
    }
}
