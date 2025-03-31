using LosTomates.PetHolidays.Reviews.Application.Shared;
using LosTomates.PetHolidays.Reviews.Data.Entities;
using LosTomates.PetHolidays.Reviews.Data.Repositories;

namespace LosTomates.PetHolidays.Reviews.Application.Ratings;

public sealed class RatingService
{
    private readonly RatingRepository repository;
    private readonly RatingUpdateRepository updatesRepository;
    private readonly ReviewRepository reviewRepository;

    public RatingService(RatingRepository repository,
        RatingUpdateRepository updatesRepository,
        ReviewRepository reviewRepository)
    {
        this.repository = repository;
        this.updatesRepository = updatesRepository;
        this.reviewRepository = reviewRepository;
    }

    public async Task<IReadOnlyList<RatingViewModel>> GetAllRatings()
    {
        var ratings = await repository.GetAll();

        var views = ratings.Select(r => new RatingViewModel
        {
            Entity = new EntityViewModel
            {
                Id = r.Entity.Id,
                Type = r.Entity.Type
            },
            Reviews = r.Reviews,
            Average = r.Average
        }).ToList();

        return views.AsReadOnly();
    }

    public async Task<EntityRatingViewModel> GetEntityRating(EntityDto entity)
    {
        var rating = await repository.GetFirstWhere(x => x.Entity.Type == entity.Type
                                                      && x.Entity.Id == entity.Id);
        return new()
        {
            Reviews = rating?.Reviews ?? 0,
            Average = rating?.Average ?? 0
        };
    }

    public async Task UpdateRatings()
    {
        var udpates = await updatesRepository.GetAll();
        var grouped = udpates.GroupBy(x => x.Entity);

        foreach (var group in grouped)
        {
            var entity = group.Key;
            var lastUpdate = group.Max(x => x.Date);
            var reviews = await reviewRepository.GetWhere(x => x.Entity.Type == entity.Type
                                                            && x.Entity.Id == entity.Id
                                                            && x.Date <= lastUpdate);
            var average = reviews.Average(x => x.Stars);

            var rating = new Rating
            {
                Entity = entity,
                Reviews = reviews.Count(),
                Average = (float) Math.Round(average, 1)
            };

            var dbEntry = await repository.GetFirstWhere(x => x.Entity.Type == entity.Type
                                                            && x.Entity.Id == entity.Id);
            if (dbEntry == null)
            {
                await repository.Add(rating);
            }
            else
            {
                rating.Id = dbEntry.Id;
                await repository.Update(rating);
            }

            foreach (var update in group)
                await updatesRepository.Delete(update);            
        }
    }
}
