using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.DataAccess.DataSeed;

public sealed class SeedService
{
    private readonly ApplicationDbContext dbContext;

    public SeedService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void ApplyMigrations()
        => dbContext.Database.Migrate();

    public void SeedData()
    {
        AddHotels();
        AddUsers();

        dbContext.SaveChanges();
    }

    private void AddHotels()
    {
        foreach (var entity in FakeData.Hotels)
        {
            if (dbContext.Hotels.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
    }
    private void AddUsers()
    {
        foreach (var entity in FakeData.Users)
        {
            if (dbContext.Users.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
    }
}
