using LosTomates.PetHolidays.WebApi.Extensions;

namespace LosTomates.PetHolidays.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        AddServices(builder.Services, builder.Configuration);

        var app = builder.Build();

        ConfigurePipeline(app);

        app.Run();
    }

    // Add services to the container.
    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthorization();
        services.AddApplicationServices();
        services.AddSwagger();
    }

    // Configure the HTTP request pipeline.
    private static void ConfigurePipeline(WebApplication app)
    {

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseAuthorization();

        app.MapApplicationEndpoints();
    }
}
