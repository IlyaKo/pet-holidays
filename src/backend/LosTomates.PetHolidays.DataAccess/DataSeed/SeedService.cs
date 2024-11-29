using LosTomates.PetHolidays.Core.Domain.Hotels;
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
    }

    private void AddHotels()
    {
        foreach (var entity in FakeData.Hotels)
        {
            if (dbContext.Hotels.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
        dbContext.SaveChanges();

        ResetSequence<Hotel>();

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

    private void ResetSequence<TEntity>() where TEntity : class
    {
        var tableName = dbContext.Model.FindEntityType(typeof(TEntity))?.GetTableName();

        var maxId = dbContext.Set<TEntity>()
                             .AsNoTracking()
                             .Max(e => EF.Property<int>(e, "Id"));

        var sql = $@"SELECT setval(pg_get_serial_sequence('""{tableName}""', 'Id'), {maxId});";

        dbContext.Database.ExecuteSqlRaw(sql);
    }
}
