using LosTomates.PetHolidays.Application.Hotels;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class HotelEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/hotels")
                          .WithTags("Hotel management")
                          .WithOpenApi();

        mapGroup.MapGet(String.Empty, async (IHotelService service) => await service.GetAll())
                .WithSummary("Get list of hotels")
                .WithDescription("Return list with all available hotels")
                .Produces(StatusCodes.Status200OK);
    }
}

