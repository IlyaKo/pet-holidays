using LosTomates.PetHolidays.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LosTomates.PetHolidays.Tests;

public sealed class FixtureWithInMemoryDatabase : IDisposable
{
    public IServiceProvider ServiceProvider { get; }

    public FixtureWithInMemoryDatabase()
    {
        var configurationBuilder = new ConfigurationBuilder();
        var inMemorySettings = new Dictionary<string, string?> 
        {
            {"ConnectionStrings:CoreDb", "Empty connection string"},
            {"JwtSettings:SecretKey", "Secret key"}
        };
        configurationBuilder.AddInMemoryCollection(inMemorySettings);
        var configuration = configurationBuilder.Build();

        var services = new ServiceCollection();
        WebApi.Program.AddServices(services, configuration);

        var realDbContext = services.SingleOrDefault(d => d.ServiceType == typeof(DbContextOptions<ApplicationDbContext>));
        if (realDbContext != null)
            services.Remove(realDbContext);

        services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase(databaseName: "TestDatabase"));

        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
    }
}