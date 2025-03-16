using LosTomates.PetHolidays.Reviews.Data.Entities;
using LosTomates.PetHolidays.Reviews.Data.Repositories;

namespace LosTomates.PetHolidays.Reviews.Application.Reviews;

public sealed class ReviewService
{
    private readonly ReviewRepository repository;

    public ReviewService(ReviewRepository repository)
    {
        this.repository = repository;
    }

    public async Task<IEnumerable<Review>> GetAllReviews()
        => await repository.GetAll();
    
    public async Task AddReview(ReviewDto dto)
    {
        var review = new Review
        {
            Entity = new() 
            { 
                Id = dto.Entity.Id, 
                Type = dto.Entity.Type
            },
            User = new() 
            { 
                Id = dto.User.Id,
                Name = dto.User.Name
            },
            Date = DateTime.UtcNow,
            Stars = dto.Stars,
            Comment = dto.Comment
        };

        await repository.Add(review);
    }

    public async Task DeleteReview(string id)
    {
        await repository.Delete(id);
    }
}
