
namespace LosTomates.PetHolidays.Application.Users;

public interface ICurrentUserSetter
{
    void Set(string userId);

    void Remove();
}