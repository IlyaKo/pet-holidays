namespace LosTomates.PetHolidays.Core.Application.Users;

public sealed record CreateResponse
{
    public required string Username { get; set; }
}
