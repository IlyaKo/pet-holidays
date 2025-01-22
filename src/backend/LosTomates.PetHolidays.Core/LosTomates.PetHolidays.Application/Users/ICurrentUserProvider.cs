
namespace LosTomates.PetHolidays.Application.Users;

public interface ICurrentUserProvider
{
    bool LoggedIn { get; }

    string GetUserId();
}