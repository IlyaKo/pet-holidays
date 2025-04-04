using LosTomates.PetHolidays.Reviews.Application.Shared;

namespace LosTomates.PetHolidays.Reviews.Application.Reviews;

public sealed record ReviewDto
{
    public required EntityDto Entity { get; set; }

    public required UserDto User { get; set; }

    public int Stars { get; set; }

    public string? Comment { get; set; }
}
