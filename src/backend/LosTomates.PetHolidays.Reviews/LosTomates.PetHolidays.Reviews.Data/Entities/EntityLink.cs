namespace LosTomates.PetHolidays.Reviews.Data.Entities;

public sealed record EntityLink
{
    public required string Id { get; set; }

    public required string Type { get; set; }
}
