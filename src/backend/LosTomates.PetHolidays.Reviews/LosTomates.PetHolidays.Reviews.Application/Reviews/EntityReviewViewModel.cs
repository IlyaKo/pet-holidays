using LosTomates.PetHolidays.Reviews.Application.Shared;

namespace LosTomates.PetHolidays.Reviews.Application.Reviews;

public sealed class EntityReviewViewModel
{
    public required UserViewModel User { get; set; }

    public DateTime Date { get; set; }

    public int Stars { get; set; }

    public string? Comment { get; set; }
}
