using LosTomates.PetHolidays.Auth.Application.Extensions;
using LosTomates.PetHolidays.Auth.Core.Domain.Users;
using LosTomates.PetHolidays.Auth.DataAccess;
using LosTomates.PetHolidays.Auth.DataAccess.DataSeed;
using LosTomates.PetHolidays.Auth.WebApi.Extensions;
using LosTomates.PetHolidays.Auth.WebApi.Middleware;
using Microsoft.AspNetCore.Identity;

//namespace LosTomates.PetHolidays.Auth.WebApi;

var builder = WebApplication.CreateBuilder(args);

AddServices(builder.Services, builder.Configuration);

var app = builder.Build();

SeedData(app.Services);
ConfigurePipeline(app);

app.Run();

// Add services to the container.
static void AddServices(IServiceCollection services, IConfiguration configuration)
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

    services.AddJwtAuthentication(configuration);

    services.AddScoped<SignInManager<User>>();
}

// Configure the HTTP request pipeline.
static void ConfigurePipeline(WebApplication app)
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
static void SeedData(IServiceProvider services)
{
    using var scope = services.CreateScope();
    var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

    seedService.ApplyMigrations();
    seedService.SeedData();
}