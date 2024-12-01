using LosTomates.PetHolidays.Application.PetTypes;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class PetTypeEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/pet-types")
                          .WithTags("Pet types management")
                          .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IPetTypeService service) => await service.GetAll())
                .WithSummary("Get list of pet types")
                .WithDescription("Return a list with all active pet types")
                .Produces<List<PetTypeView>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{petTypeId:int}", async (IPetTypeService service, int petTypeId) =>
        {
            return await service.GetById(petTypeId);
        })
        .WithSummary("Get a pet type by its Id")
        .WithDescription("Return a pet type including not active ones")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IPetTypeService service, PetTypeEditDto dto) =>
        {
            return await service.Create(dto);
        })
        .WithSummary("Create a new pet type")
        .WithDescription("Return an id of a created pet type")
        .Produces<PetTypeView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{petTypeId:int}", async (IPetTypeService service, int petTypeId, PetTypeEditDto dto) =>
        {
            await service.Update(petTypeId, dto);
        })
        .WithSummary("Update a pet type record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{petTypeId:int}", async (IPetTypeService service, int petTypeId) =>
        {
            await service.Delete(petTypeId);
        })
        .WithSummary("Delete a pet type record")
        .Produces(StatusCodes.Status200OK);
    }
}
