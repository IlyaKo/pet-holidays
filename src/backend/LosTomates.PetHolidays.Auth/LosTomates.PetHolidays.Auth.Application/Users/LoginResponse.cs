namespace LosTomates.PetHolidays.Auth.Application.Users;

public sealed record LoginResponse
{
    public required string Token { get; set; }
    public required string Username { get; set; }
}
