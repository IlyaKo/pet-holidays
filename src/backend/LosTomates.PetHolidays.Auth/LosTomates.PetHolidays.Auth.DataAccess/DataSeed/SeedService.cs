
using LosTomates.PetHolidays.Auth.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Auth.DataAccess.DataSeed;

public sealed class SeedService
{
    private readonly ApplicationDbContext dbContext;

    public SeedService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void ApplyMigrations() => dbContext.Database.Migrate();

    public void SeedData()
    {
        AddUsers();
    }

    private void AddUsers()
    {
        foreach (var entity in FakeData.Users)
        {
            if (dbContext.Users.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
        dbContext.SaveChanges();
    }
}