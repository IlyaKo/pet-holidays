using LosTomates.PetHolidays.Application.Hotels;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class HotelEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/hotels")
                          .WithTags("Hotel management")
                          .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IHotelService service) => await service.GetAll())
                .WithSummary("Get list of hotels")
                .WithDescription("Return a list with all active hotels")
                .Produces<List<HotelView>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{hotelId:int}", async (IHotelService service, int hotelId) =>
        {
            var entityView = await service.GetById(hotelId);
            if (entityView is null)
                return Results.NotFound("Can't find a record with the id " + hotelId);
            else
                return Results.Ok(entityView);
        })
        .WithSummary("Get a hotel by its Id")
        .WithDescription("Return a hotel including not active ones")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IHotelService service, HotelEditDto dto) => await service.Create(dto))
                .WithSummary("Create a new hotel")
                .WithDescription("Return an id of a created hotel")
                .Produces<HotelView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{hotelId:int}", async (IHotelService service, int hotelId, HotelEditDto dto)
            => await service.Update(hotelId, dto))
        .WithSummary("Update a hotel record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{hotelId:int}", async (IHotelService service, int hotelId) => await service.Delete(hotelId))
                .WithSummary("Delete a hotel record")
                .Produces(StatusCodes.Status200OK);
    }
}

