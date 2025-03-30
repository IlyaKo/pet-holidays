using LosTomates.PetHolidays.Reviews.Application.Ratings;

namespace LosTomates.PetHolidays.Reviews.WebApi.Endpoints;

public static class RatingEndpoints
{
    public static void Map(IEndpointRouteBuilder app)
    {
        var mapGroup = app.MapGroup("api/ratings")
                          .WithTags("Ratings");

        mapGroup.MapGet(string.Empty, (RatingService service) => service.GetAllRatings())
                .WithSummary("Get ratings")
                .WithDescription("Get list of all ratings")
                .Produces(StatusCodes.Status200OK);

        mapGroup.MapGet("{entityType:required}/{entityId:required}",
            async (RatingService service, string entityType, string entityId)
            => await service.GetEntityRating(new() { Type = entityType, Id = entityId }))
                .WithSummary("Get entity rating")
                .Produces(StatusCodes.Status200OK);
    }
}

