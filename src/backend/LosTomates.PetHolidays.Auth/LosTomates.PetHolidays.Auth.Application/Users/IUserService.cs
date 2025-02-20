using System.Security.Claims;

namespace LosTomates.PetHolidays.Auth.Application.Users;

public interface IUserService
{
    Task<UserView> GetById(string userId);

    Task<LoginResponse> Create(UserEditDto dto);

    Task Update(string entityId, UserEditDto dto);

    (string UserId, string UserName) GetUserFromToken(string token);

    Task<LoginResponse> Login(LoginDto dto);

    Task<(string UserId, string UserName)> CurrentUser(ClaimsPrincipal userClaims);

    Task<UserView> GetCurrentUser();
}
