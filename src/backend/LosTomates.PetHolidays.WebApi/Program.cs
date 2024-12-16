using LosTomates.PetHolidays.Application.Extensions;
using LosTomates.PetHolidays.Core.Domain.Users;
using LosTomates.PetHolidays.DataAccess;
using LosTomates.PetHolidays.DataAccess.DataSeed;
using LosTomates.PetHolidays.WebApi.Extensions;
using LosTomates.PetHolidays.WebApi.Middleware;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LosTomates.PetHolidays.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        SeedData(app.Services);
        ConfigurePipeline(app);

        app.Run();
    }

    // Add services to the container.
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddApplicationServices();
        services.AddMappings();
        services.AddDatabaseContext(configuration);
        services.AddFluentValidation();
        services.AddCors();
        services.AddSwagger();

        services.AddProblemDetails();

        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<BusinessLogicExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();

        services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        var key = Encoding.ASCII.GetBytes(configuration["JwtSettings:SecretKey"]);
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

        services.AddScoped<SignInManager<User>>();
    }

    // Configure the HTTP request pipeline.
    private static void ConfigurePipeline(WebApplication app)
    {
        app.UseExceptionHandler();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthentication();
        app.UseAuthorization();

        app.UseCors(options => options.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
        app.MapApplicationEndpoints();
    }

    // Migrate and add testing data to the database if necessary.
    private static void SeedData(IServiceProvider services)
    {
        using var scope = services.CreateScope();
        var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

        seedService.ApplyMigrations();
        seedService.SeedData();
    }
}
