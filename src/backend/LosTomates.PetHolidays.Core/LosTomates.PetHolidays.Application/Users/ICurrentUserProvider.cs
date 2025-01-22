
namespace LosTomates.PetHolidays.Core.Application.Users;

public interface ICurrentUserProvider
{
    bool LoggedIn { get; }

    string GetUserId();
}