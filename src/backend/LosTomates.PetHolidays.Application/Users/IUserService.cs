namespace LosTomates.PetHolidays.Application.Users;

public interface IUserService
{
    Task<UserView> GetById(string userId);

    Task<string> Create(UserEditDto dto);

    Task Update(string entityId, UserEditDto dto);
    (string UserId, string UserName) GetUserFromToken(string token);
    Task<string> Login(LoginDto dto);
}
