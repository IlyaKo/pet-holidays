using LosTomates.PetHolidays.Core.Application.Extensions;
using LosTomates.PetHolidays.Core.Core.Domain.Users;
using LosTomates.PetHolidays.Core.Core.FileService;
using LosTomates.PetHolidays.Core.DataAccess;
using LosTomates.PetHolidays.Core.DataAccess.DataSeed;
using LosTomates.PetHolidays.Core.WebApi.Extensions;
using LosTomates.PetHolidays.Core.WebApi.Middleware;
using Microsoft.AspNetCore.Identity;

namespace LosTomates.PetHolidays.Core.WebApi;

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
        services.AddRabbitMQ(configuration);

        services.AddProblemDetails();

        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<BusinessLogicExceptionHandler>();
        services.AddExceptionHandler<ValidationExceptionHandler>();

        services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

        services.AddJwtAuthentication(configuration);

        services.AddScoped<SignInManager<User>>();

        services.AddHttpClient<IFileServiceClient, FileServiceClient>();
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
        app.UseMiddleware<UserHandlerMiddleware>();

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
