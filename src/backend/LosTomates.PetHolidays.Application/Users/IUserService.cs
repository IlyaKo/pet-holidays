
namespace LosTomates.PetHolidays.Application.Users;

public interface IUserService
{
    Task<IReadOnlyList<UserView>> GetAll();

    Task<UserView?> GetById(int UserId);

    Task<int> Create(UserEditDto dto);

    Task Update(int entityId, UserEditDto dto);

    Task Delete(int entityId);
}
