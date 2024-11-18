namespace LosTomates.PetHolidays.Application.Users;

public sealed record UserEditDto
{
    public required string UserName { get; init; }
    public string? Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string Password { get; init; }
}
