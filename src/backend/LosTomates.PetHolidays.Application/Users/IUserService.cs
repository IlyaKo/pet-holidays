
namespace LosTomates.PetHolidays.Application.Users;

public interface IUserService
{
    Task<UserView?> GetById(string userId);

    Task<string> Create(UserEditDto dto);

    Task Update(string entityId, UserEditDto dto);
}
