using FluentValidation;
using LosTomates.PetHolidays.Core.Domain.Users;
using LosTomates.PetHolidays.Core.Exceptions;
using LosTomates.PetHolidays.DataAccess;
using LosTomates.PetHolidays.DataAccess.Migrations;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LosTomates.PetHolidays.Application.Users;

public sealed class UserService: IUserService 
{
    private readonly ApplicationDbContext dbContext;

    private readonly IValidator<UserEditDto> validateService;

    private readonly UserManager<User> _userManager;
    public UserService(ApplicationDbContext dbContext, IValidator<UserEditDto> validateService,
                       UserManager<User> userManager)
    {
        this.dbContext = dbContext;
        this.validateService = validateService;
        this._userManager = userManager;
    }
    public async Task<UserView?> GetById(string entityId)
    {
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(User), entityId.ToString());

        return entity.Adapt<UserView>();
    }

    public async Task<string> Create(UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);
        var user = dto.Adapt<User>();
        var result = await _userManager.CreateAsync(user, dto.Password);

        if (result.Succeeded)
        {
            return user.Id;
        }

        var errors = result.Errors.Select(e => e.Description).ToList();
        throw new ValidationException(string.Join(", ", errors));
    }

    public async Task Update(string entityId, UserEditDto dto)
    {
        validateService.ValidateAndThrow(dto);
        var entity = await FindEntityById(entityId)
                  ?? throw new NotFoundException(nameof(User), entityId.ToString());

        dto.Adapt(entity);

        await dbContext.SaveChangesAsync();
    }

    private async Task<User?> FindEntityById(string entityId)
    {
        return await dbContext.Users.FirstOrDefaultAsync(x => x.Id == entityId);
    }
}
