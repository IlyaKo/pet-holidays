using LosTomates.PetHolidays.Reviews.Application.DataSeed;
using LosTomates.PetHolidays.Reviews.WebApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

await SeedData(app.Services);

ConfigurePipeline(app);

app.Run();

void AddServices(IServiceCollection services, IConfiguration configuration)
{
    services.AddSwagger();
    services.AddCors();
    services.AddApplicationServices();
    services.AddBackgroundServices();
    services.AddDatabaseContext(configuration);
}

void ConfigurePipeline(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors(options => options.AllowAnyOrigin()
                                  .AllowAnyHeader()
                                  .AllowAnyMethod());

    app.MapApplicationEndpoints();
}

async Task SeedData(IServiceProvider services)
{
    var scope = services.CreateScope();
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

    await seedService.SeedComments();
}