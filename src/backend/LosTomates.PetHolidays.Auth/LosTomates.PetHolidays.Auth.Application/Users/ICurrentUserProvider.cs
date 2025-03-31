
namespace LosTomates.PetHolidays.Auth.Application.Users;

public interface ICurrentUserProvider
{
    bool LoggedIn { get; }

    string GetUserId();
}