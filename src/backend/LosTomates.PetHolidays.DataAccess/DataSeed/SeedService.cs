using LosTomates.PetHolidays.Core.Domain.Bookings;
using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using LosTomates.PetHolidays.DataAccess.Migrations;
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
        AddRoomTypes();
        AddRooms();
        AddPetTypes();
        AddPets();
        AddBookings();
    }

    private void AddPets()
    {
        foreach (var entity in FakeData.Pets)
        {
            if (dbContext.Pets.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }

        dbContext.SaveChanges();

        ResetSequence<Pet>();
    }

    private void AddPetTypes()
    {
        foreach (var entity in FakeData.PetTypes)
        {
            if (dbContext.PetTypes.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }

        dbContext.SaveChanges();

        ResetSequence<PetType>();
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

    private void AddRoomTypes()
    {
        foreach (var entity in FakeData.RoomTypes)
        {
            if (dbContext.RoomTypes.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
        dbContext.SaveChanges();

        ResetSequence<RoomType>();
    }

    private void AddRooms()
    {
        foreach (var entity in FakeData.Rooms)
        {
            if (dbContext.Rooms.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
        dbContext.SaveChanges();

        ResetSequence<Room>();
    }

    private void AddBookings()
    {
        foreach (var entity in FakeData.Bookings)
        {
            if (dbContext.Bookings.Any(x => x.Id == entity.Id))
                continue;

            dbContext.Add(entity);
        }
        dbContext.SaveChanges();

        ResetSequence<Booking>();
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
