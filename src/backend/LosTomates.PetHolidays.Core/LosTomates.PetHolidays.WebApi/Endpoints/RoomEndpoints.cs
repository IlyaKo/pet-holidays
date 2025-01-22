using LosTomates.PetHolidays.Application.Rooms;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class RoomEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/hotels/{hotelId:int}/rooms")
                          .WithTags("Room management")
        .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IRoomService service, int hotelId) => await service.GetAll(hotelId))
                .WithSummary("Get list of rooms")
                .WithDescription("Return a list of rooms of a given hotel")
                .Produces<List<RoomView>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{roomId:int}", async (IRoomService service, int hotelId, int roomId) =>
        {
            var entityView = await service.GetById(hotelId, roomId);
            return Results.Ok(entityView);
        })
        .WithSummary("Get a room by its id and its hotel id")
        .WithDescription("Return a room by its id and its hotel id")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IRoomService service, int hotelId, RoomEditDto dto) =>
        {
            return await service.Create(hotelId, dto);
        })
        .WithSummary("Create a new room in a given hotel")
        .WithDescription("Return an id of a created room")
        .Produces<RoomView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{roomId:int}", async (IRoomService service, int hotelId, int roomId, RoomEditDto dto) =>
        {
            await service.Update(hotelId, roomId, dto);
        })
        .WithSummary("Update a room record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{roomId:int}", async (IRoomService service, int hotelId, int roomId) =>
        {
            await service.Delete(hotelId, roomId);
        })
        .WithSummary("Delete a room record")
        .Produces(StatusCodes.Status200OK);
    }
}
