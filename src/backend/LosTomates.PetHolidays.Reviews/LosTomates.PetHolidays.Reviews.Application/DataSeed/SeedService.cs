using LosTomates.PetHolidays.Reviews.Application.Reviews;
using LosTomates.PetHolidays.Reviews.Application.Shared;

namespace LosTomates.PetHolidays.Reviews.Application.DataSeed;

public sealed class SeedService(ReviewService reviewService)
{
    private readonly ReviewService _reviewService = reviewService;

    public async Task SeedComments()
    {
        var entity = new EntityDto
        {
            Id = "1",
            Type = "hotels"
        };

        var reviews = await _reviewService.GetEntityReviews(entity);
        if (reviews.Count > 0)
            return;

        var comments = new List<ReviewDto>()
        {
            new()
            {
                Entity = entity,
                User = new UserDto { Id = "b10deb6c-63cb-4d73-8cbd-65203a5000db", Name = "Maxim" },
                Stars = 5,
                Comment = "Great place, my dog was happy!"
            },
            new()
            {
                Entity = entity,
                User = new UserDto { Id = "1fb17ad5-f507-4683-8be7-4fe276d1f086", Name = "Elena" },
                Stars = 4,
                Comment = "Not great, but not terrible."
            },
            new()
            {
                Entity = entity,
                User = new UserDto { Id = "390319EE-19CD-42A2-A192-429234186F52", Name = "Vasya" },
                Stars = 1,
                Comment = "Didn't like the service at all."
            },
        };

        foreach (var comment in comments)
            await _reviewService.AddReview(comment);
    }
}
