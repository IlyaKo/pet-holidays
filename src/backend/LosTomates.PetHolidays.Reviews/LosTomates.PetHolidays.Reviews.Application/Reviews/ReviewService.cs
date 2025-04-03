using LosTomates.PetHolidays.Reviews.Application.Shared;
using LosTomates.PetHolidays.Reviews.Data.Entities;
using LosTomates.PetHolidays.Reviews.Data.Repositories;

namespace LosTomates.PetHolidays.Reviews.Application.Reviews;

public sealed class ReviewService
{
    private readonly ReviewRepository repository;
    private readonly RatingUpdateRepository ratingUpdatesRepository;

    public ReviewService(ReviewRepository repository,
        RatingUpdateRepository ratingUpdatesRepository)
    {
        this.repository = repository;
        this.ratingUpdatesRepository = ratingUpdatesRepository;
    }

    public async Task<IReadOnlyList<ReviewViewModel>> GetAllReviews()
    {
        var reviews = await repository.GetAll();
        var views = reviews.Select(r => new ReviewViewModel
        {
            Entity = new EntityViewModel
            {
                Id = r.Entity.Id,
                Type = r.Entity.Type
            },
            User = new UserViewModel
            {
                Id = r.User.Id,
                Name = r.User.Name
            },
            Date = r.Date,
            Stars = r.Stars,
            Comment = r.Comment
        }).ToList();

        return views.AsReadOnly();
    }

    public async Task AddReview(ReviewDto dto)
    {
        var date = DateTime.UtcNow;
        var entity = new EntityLink
        {
            Id = dto.Entity.Id,
            Type = dto.Entity.Type
        };
        var review = new Review
        {
            Entity = entity,
            User = new() 
            { 
                Id = dto.User.Id,
                Name = dto.User.Name
            },
            Date = date,
            Stars = dto.Stars,
            Comment = dto.Comment
        };

        await repository.Add(review);

        var newReview = new RatingUpdate
        {
            Entity = entity,
            Date = date,
        };
        await ratingUpdatesRepository.Add(newReview);
    }

    public async Task DeleteReview(string id)
    {
        await repository.Delete(id);
        // Todo add rating repository update entry
    }

    public async Task<IReadOnlyList<EntityReviewViewModel>> GetEntityReviews(EntityDto entity)
    {
        var reviews = await repository.GetWhere(x => x.Entity.Id == entity.Id
                                                  && x.Entity.Type == entity.Type);

        var views = reviews.Select(r => new EntityReviewViewModel
        {
            User = new UserViewModel
            {
                Id = r.User.Id,
                Name = r.User.Name
            },
            Stars = r.Stars,
            Comment = r.Comment,
            Date = r.Date
        }).ToList();

        return views.AsReadOnly();
    }

}
