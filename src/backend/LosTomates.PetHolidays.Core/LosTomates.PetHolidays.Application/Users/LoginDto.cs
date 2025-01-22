namespace LosTomates.PetHolidays.Application.Users;

public sealed record LoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
