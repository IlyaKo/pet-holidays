using LosTomates.PetHolidays.Core.Core.Domain.Bookings;
using LosTomates.PetHolidays.Core.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.Core.Domain.Rooms;
using LosTomates.PetHolidays.Core.Core.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Core.DataAccess;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Hotel> Hotels => Set<Hotel>();

    public DbSet<RoomType> RoomTypes => Set<RoomType>();

    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<PetType> PetTypes => Set<PetType>();

    public DbSet<Pet> Pets => Set<Pet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.Property(x => x.Description)
                  .HasMaxLength(DatabaseConstrains.DescriptionMaxLength);

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
                  .HasForeignKey(x => x.RoomTypeId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Booking>(entity =>
        {

        });

        modelBuilder.Entity<PetType>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.HasIndex(p => p.Name)
                  .IsUnique();
        });

        modelBuilder.Entity<Pet>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.HasOne(x => x.PetType)
                  .WithMany()
                  .HasForeignKey(x => x.PetTypeId)
                  .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(x => x.PetOwner)
                  .WithMany()
                  .HasForeignKey(x => x.PetOwnerId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}