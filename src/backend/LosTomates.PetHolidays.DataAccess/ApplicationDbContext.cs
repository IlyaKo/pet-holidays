using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.DataAccess;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext(options)
{
    public DbSet<Hotel> Hotels { get; set; }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Hotel>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.Property(x => x.Description)
                  .HasMaxLength(DatabaseConstrains.DescriptionMaxLength);
        });
        modelBuilder.Entity<User>();
    }
}
