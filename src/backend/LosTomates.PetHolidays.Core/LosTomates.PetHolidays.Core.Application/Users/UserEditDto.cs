namespace LosTomates.PetHolidays.Core.Application.Users;

public sealed record UserEditDto
{
    public required string UserId { get; init; }
    public required string UserName { get; init; }
}
