using System.Security.Claims;

namespace LosTomates.PetHolidays.Auth.Application.Users;

public interface IUserService
{
    Task<UserView> GetById(string userId);

    Task<LoginResponse> Create(UserEditDto dto);

    Task<LoginResponse> Login(LoginDto dto);

    Task<(string UserId, string UserName)> CurrentUser(ClaimsPrincipal userClaims);

    Task<UserView> GetCurrentUser();
}
