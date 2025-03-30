using LosTomates.PetHolidays.Reviews.Application.Shared;

namespace LosTomates.PetHolidays.Reviews.Application.Ratings;

public sealed class RatingViewModel
{
    public required EntityViewModel Entity { get; set; }

    public int Reviews { get; set; }

    public float Average { get; set; }
}
