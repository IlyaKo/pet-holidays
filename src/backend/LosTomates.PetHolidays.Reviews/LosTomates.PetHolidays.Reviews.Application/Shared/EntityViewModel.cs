namespace LosTomates.PetHolidays.Reviews.Application.Shared;

public sealed record EntityViewModel
{
    public required string Id { get; set; }

    public required string Type { get; set; }
}
