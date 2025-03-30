namespace LosTomates.PetHolidays.Reviews.Application.Shared;

public sealed record UserViewModel
{
    public int Id { get; set; }

    public required string Name { get; set; }
}
