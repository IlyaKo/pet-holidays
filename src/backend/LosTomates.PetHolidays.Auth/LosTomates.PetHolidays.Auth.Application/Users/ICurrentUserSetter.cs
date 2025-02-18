
namespace LosTomates.PetHolidays.Auth.Application.Users;

public interface ICurrentUserSetter
{
    void Set(string userId);

    void Remove();
}