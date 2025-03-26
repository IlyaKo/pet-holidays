namespace LosTomates.PetHolidays.Core.Application.Users;

public sealed record UserEditDto
{
    public required string UserName { get; init; }
}
