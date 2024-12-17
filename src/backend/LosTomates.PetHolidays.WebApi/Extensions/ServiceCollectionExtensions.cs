using FluentValidation;
using LosTomates.PetHolidays.Application.Hotels;
using LosTomates.PetHolidays.Application.Rooms;
using LosTomates.PetHolidays.Application.RoomTypes;
using LosTomates.PetHolidays.Application.Users;
using LosTomates.PetHolidays.DataAccess;
using LosTomates.PetHolidays.DataAccess.DataSeed;
using Microsoft.EntityFrameworkCore;
using FluentValidation;

using LosTomates.PetHolidays.Application.Validations;
using LosTomates.PetHolidays.Application.PetTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LosTomates.PetHolidays.WebApi.Extensions;

internal static class ServiceCollectionExtensions
{
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    internal static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            //for test tokens
            options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new()
            {
                Type = SecuritySchemeType.Http,
                Scheme = JwtBearerDefaults.AuthenticationScheme,
                BearerFormat = "JWT",
                Description = "JWT Authorization header using the Bearer scheme"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new() { Type = ReferenceType.SecurityScheme, Id = JwtBearerDefaults.AuthenticationScheme }
                    },
                    Array.Empty<string>()
                }
            });
        });

        return services;
    }

    internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IHotelService, HotelService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPetTypeService, PetTypeService>();
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

    internal static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var configuredKey = configuration["JwtSettings:SecretKey"]
            ?? throw new ApplicationException("An environment variable named JwtSettings__SecretKey is not set");

        var key = Encoding.ASCII.GetBytes(configuredKey);
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        return services;
    }
}
