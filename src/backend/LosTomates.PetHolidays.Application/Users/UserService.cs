using LosTomates.PetHolidays.Application.Users;
using LosTomates.PetHolidays.Core.Domain.Users;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace LosTomates.PetHolidays.Application.Users;

public sealed class UserService : IUserService
{
    private readonly ApplicationDbContext dbContext;

    public UserService(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IReadOnlyList<UserView>> GetAll()
        => await dbContext.Users
                          .Select(x => new UserView(x))
                          .ToListAsync();

    public async Task<UserView?> GetById(int entityId)
        => await dbContext.Users
                          .Where(x => x.Id == entityId)
                          .Select(x => new UserView(x))
                          .FirstOrDefaultAsync();

    public async Task<int> Create(UserEditDto dto)
    {
        var entity = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Phone = dto.Phone,
            Password = dto.Password,
            CreatedDate = DateTime.Now,
        };

        dbContext.Add(entity);
        await dbContext.SaveChangesAsync();

        return entity.Id;
    }

    public async Task Update(int entityId, UserEditDto dto)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException("User", entityId.ToString());

        entity.Name = dto.Name;
        entity.Email = dto.Email;
        entity.Phone = dto.Phone;
        entity.Password = dto.Password;

        await dbContext.SaveChangesAsync();
    }
    public async Task Delete(int entityId)
    {
        var entity = await FindEntityById(entityId);

        if (entity is null)
            return;

        dbContext.Remove(entity);
        await dbContext.SaveChangesAsync();
    }

    private async Task<User?> FindEntityById(int entityId)
        => await dbContext.Users.FirstOrDefaultAsync(x => x.Id == entityId);
}
