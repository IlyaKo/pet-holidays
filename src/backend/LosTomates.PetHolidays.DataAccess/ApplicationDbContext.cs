using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Rooms;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.DataAccess;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Hotel> Hotels => Set<Hotel>();

    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    public DbSet<Room> Rooms => Set<Room>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.Property(x => x.Description)
                  .HasMaxLength(DatabaseConstrains.DescriptionMaxLength);

            entity.HasMany(x => x.RoomTypes)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId);

            entity.HasMany(x => x.Rooms)
                .WithOne(x => x.Hotel)
                .HasForeignKey(x => x.HotelId);
        });

        modelBuilder.Entity<RoomType>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.Property(x => x.Description)
                  .HasMaxLength(DatabaseConstrains.DescriptionMaxLength);
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.Property(x => x.Location)
                  .HasMaxLength(DatabaseConstrains.AddressMaxLength);

            entity.Property(x => x.Description)
                  .HasMaxLength(DatabaseConstrains.DescriptionMaxLength);

            entity.HasOne(x => x.RoomType)
                .WithMany(x => x.Rooms)
                .HasForeignKey(x => x.RoomTypeId);
        });
    }
}
