using System.Security.Authentication;

namespace LosTomates.PetHolidays.Auth.Application.Users;

public sealed class CurrentUserProvider : ICurrentUserProvider, ICurrentUserSetter
{
    public bool LoggedIn => _userId is not null;

    private string? _userId;

    public string GetUserId()
    {
        if (_userId is null)
            throw new AuthenticationException("You need to be logged in");

        return _userId;
    }

    public void Set(string userId)
    {
        _userId = userId;
    }
    
    public void Remove()
    {
        _userId = null;
    }
}
