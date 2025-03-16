using LosTomates.PetHolidays.Reviews.Data.Repositories;

namespace LosTomates.PetHolidays.Reviews.Application.Ratings;

public sealed class RatingService
{
    private readonly RatingRepository repository;

    public RatingService(RatingRepository repository)
    {
        this.repository = repository;
    }
}
