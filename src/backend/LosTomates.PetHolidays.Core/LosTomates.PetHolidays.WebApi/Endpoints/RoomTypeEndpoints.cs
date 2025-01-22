using LosTomates.PetHolidays.Application.RoomTypes;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class RoomTypeEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/room-types")
                          .WithTags("Room type management")
        .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IRoomTypeService service) => await service.GetAll())
                .WithSummary("Get list of room types")
                .WithDescription("Return a list of room types")
                .Produces<List<RoomTypeView>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{roomTypeId:int}", async (IRoomTypeService service, int roomTypeId) =>
        {
            var entityView = await service.GetById(roomTypeId);
            return Results.Ok(entityView);
        })
        .WithSummary("Get a room type by its id")
        .WithDescription("Return a room type by its id")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IRoomTypeService service, RoomTypeEditDto dto) =>
        {
            return await service.Create(dto);
        })
        .WithSummary("Create a new room type")
        .WithDescription("Return an id of a created room type")
        .Produces<RoomTypeView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{roomTypeId:int}", async (IRoomTypeService service, int roomTypeId, RoomTypeEditDto dto) =>
        {
            await service.Update(roomTypeId, dto);
        })
        .WithSummary("Update a room type record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{roomTypeId:int}", async (IRoomTypeService service, int roomTypeId) =>
        {
            await service.Delete(roomTypeId);
        })
        .WithSummary("Delete a room type record")
        .Produces(StatusCodes.Status200OK);
    }
}
