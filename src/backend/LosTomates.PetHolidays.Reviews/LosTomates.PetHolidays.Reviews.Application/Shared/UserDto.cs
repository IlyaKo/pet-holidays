namespace LosTomates.PetHolidays.Reviews.Application.Shared;

public sealed record UserDto
{
    public int Id { get; set; }

    public required string Name { get; set; }
}
