namespace LosTomates.PetHolidays.Reviews.Application.Shared;

public sealed record EntityDto
{
    public required string Id { get; set; }

    public required string Type { get; set; }
}
