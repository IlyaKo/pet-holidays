namespace LosTomates.PetHolidays.Reviews.Application.Shared;

public sealed record UserDto
{
    public required string Id { get; set; }

    public required string Name { get; set; }
}
