using LosTomates.PetHolidays.Application.RoomTypes;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class RoomTypeEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/hotels/{hotelId:int}/room-types")
                          .WithTags("Room type management")
        .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IRoomTypeService service, int hotelId) => await service.GetAll(hotelId))
                .WithSummary("Get list of room types")
                .WithDescription("Return a list of room types of a given hotel")
                .Produces<List<RoomTypeView>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{roomTypeId:int}", async (IRoomTypeService service, int hotelId, int roomTypeId) =>
        {
            var entityView = await service.GetById(hotelId, roomTypeId);
            return Results.Ok(entityView);
        })
        .WithSummary("Get a room type by its id and its hotel id")
        .WithDescription("Return a room type by its id and its hotel id")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IRoomTypeService service, int hotelId, RoomTypeEditDto dto) =>
        {
            return await service.Create(hotelId, dto);
        })
        .WithSummary("Create a new room type in a given hotel")
        .WithDescription("Return an id of a created room type")
        .Produces<RoomTypeView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{roomTypeId:int}", async (IRoomTypeService service, int hotelId, int roomTypeId, RoomTypeEditDto dto) =>
        {
            await service.Update(hotelId, roomTypeId, dto);
        })
        .WithSummary("Update a room type record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{roomTypeId:int}", async (IRoomTypeService service, int hotelId, int roomTypeId) =>
        {
            await service.Delete(hotelId, roomTypeId);
        })
        .WithSummary("Delete a room type record")
        .Produces(StatusCodes.Status200OK);
    }
}
