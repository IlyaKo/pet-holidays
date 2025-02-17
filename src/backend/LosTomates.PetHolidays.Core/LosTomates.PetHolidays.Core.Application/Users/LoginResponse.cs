namespace LosTomates.PetHolidays.Core.Core.Users;

public sealed record LoginResponse
{
    public required string Token { get; set; }
    public required string Username { get; set; }
}
