using LosTomates.PetHolidays.Core.Domain.Hotels;
using LosTomates.PetHolidays.Core.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.DataAccess;

public sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
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

        modelBuilder.Entity<User>(entity =>
        {
            entity.Property(x => x.Name)
                  .HasMaxLength(DatabaseConstrains.NameMaxLength);

            entity.Property(x => x.Email)
                  .HasMaxLength(DatabaseConstrains.EmailMaxLength);

            entity.Property(x => x.Phone)
                  .HasMaxLength(DatabaseConstrains.PhoneMaxLength);

            entity.Property(x => x.CreatedDate);

            entity.Property(x => x.Password);
        });
    }
}
