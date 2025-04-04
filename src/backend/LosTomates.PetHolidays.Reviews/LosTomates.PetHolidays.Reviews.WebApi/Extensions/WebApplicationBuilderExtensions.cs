using LosTomates.PetHolidays.Reviews.WebApi.Endpoints;

namespace LosTomates.PetHolidays.Reviews.WebApi.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static IEndpointRouteBuilder MapApplicationEndpoints(this IEndpointRouteBuilder builder)
    {
        ReviewEndpoints.Map(builder);
        RatingEndpoints.Map(builder);

        return builder;
    }
}
