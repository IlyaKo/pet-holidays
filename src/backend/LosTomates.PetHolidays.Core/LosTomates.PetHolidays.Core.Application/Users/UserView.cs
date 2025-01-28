namespace LosTomates.PetHolidays.Core.Application.Users;

public sealed class UserView
{
    public required string UserName { get; init; }
    public string? Email { get; init; }
    public required string PhoneNumber { get; init; }
}
