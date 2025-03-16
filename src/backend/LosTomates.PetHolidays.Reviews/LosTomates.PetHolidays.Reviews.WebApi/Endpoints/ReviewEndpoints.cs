using LosTomates.PetHolidays.Reviews.Application.Reviews;

namespace LosTomates.PetHolidays.Reviews.WebApi.Endpoints;

public static class ReviewEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup("api/reviews")
                          .WithTags("Reviews");

        mapGroup.MapGet(string.Empty, (ReviewService service) => service.GetAllReviews())
                .WithSummary("Get reviews")
                .WithDescription("Get list of all reviews")
                .Produces(StatusCodes.Status200OK);
        
        mapGroup.MapPost(string.Empty, (ReviewService service, ReviewDto dto) => service.AddReview(dto))
                .WithSummary("Add a review")
                .WithDescription("Add a review. It needs some time to get counted in a rating")
                .Produces(StatusCodes.Status200OK);
    }
}

