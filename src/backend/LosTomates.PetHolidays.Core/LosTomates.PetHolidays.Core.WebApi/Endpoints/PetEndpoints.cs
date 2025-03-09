using LosTomates.PetHolidays.Core.Application.Pets;
using LosTomates.PetHolidays.Core.Application.Users;

namespace LosTomates.PetHolidays.Core.WebApi.Endpoints;

public static class PetEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/pets")
                          .WithTags("Pets management")
                          .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IPetService service, ICurrentUserProvider currentUserProvider) =>
        {
            var userId = currentUserProvider.GetUserId();
            var pets = await service.GetByUserId(userId);
            return Results.Ok(pets);
        })
        .WithSummary("Get user's pets")
        .RequireAuthorization();

        mapGroup.MapGet("{petId}", async (IPetService service, ICurrentUserProvider currentUserProvider, int petId) =>
        {
            var userId = currentUserProvider.GetUserId();
            var pet = await service.GetById(petId, userId);
            return Results.Ok(pet);
        })
       .WithSummary("Get pet by id")
       .RequireAuthorization();

        mapGroup.MapPost(string.Empty, async (IPetService service, ICurrentUserProvider currentUserProvider, PetEditDto dto) =>
        {
            var userId = currentUserProvider.GetUserId();
            var petId = await service.Create(userId, dto);
            return Results.Created($"/api/pets/{petId}", null);
        })
        .WithSummary("Create new pet for user")
        .RequireAuthorization();

        mapGroup.MapPut("{petId}", async (IPetService service, ICurrentUserProvider currentUserProvider, int petId, PetEditDto dto) =>
        {
            var userId = currentUserProvider.GetUserId(); 
            await service.Update(petId, userId, dto);
            return Results.NoContent();
        })
        .WithSummary("Update pet")
        .RequireAuthorization();

        mapGroup.MapDelete("{petId}", async (IPetService service, ICurrentUserProvider currentUserProvider, int petId) =>
        {
            var userId = currentUserProvider.GetUserId(); 
            await service.Delete(petId, userId);
            return Results.NoContent();
        })
        .WithSummary("Delete pet")
        .RequireAuthorization();
    }
}