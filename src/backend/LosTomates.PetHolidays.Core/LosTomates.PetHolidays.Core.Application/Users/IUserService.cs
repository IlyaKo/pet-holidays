using System.Security.Claims;

namespace LosTomates.PetHolidays.Core.Application.Users;

public interface IUserService
{
    Task<UserView> GetById(string userId);

    Task Create(string userId, UserEditDto dto);

    Task Update(string entityId, UserEditDto dto);

    (string UserId, string UserName) GetUserFromToken(string token);

    Task<(string UserId, string UserName)> CurrentUser(ClaimsPrincipal userClaims);

    Task<UserView> GetCurrentUser();
}
