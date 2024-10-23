using LosTomates.PetHolidays.DataAccess.DataSeed;
using LosTomates.PetHolidays.WebApi.Extensions;
using LosTomates.PetHolidays.WebApi.Middleware;

namespace LosTomates.PetHolidays.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        AddServices(builder.Services, builder.Configuration);

        WebApplication app = builder.Build();

        SeedData(app.Services);
        ConfigurePipeline(app);

        app.Run();
    }

    // Add services to the container.
    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddApplicationServices();
        services.AddDatabaseContext(configuration);
        services.AddFluentValidation();
        services.AddCors();
        services.AddSwagger();

        services.AddProblemDetails();

        services.AddExceptionHandler<NotFoundExceptionHandler>();
        services.AddExceptionHandler<BusinessLogicExceptionHandler>();

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

        app.UseAuthorization();
        app.UseCors(options => options.AllowAnyOrigin()
                                      .AllowAnyHeader()
                                      .AllowAnyMethod());
        app.MapApplicationEndpoints();
    }

    // Migrate and add testing data to the database if necessary.
    private static void SeedData(IServiceProvider services)
    {
        using IServiceScope scope = services.CreateScope();
        SeedService seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

        seedService.ApplyMigrations();
        seedService.SeedData();
    }
}
