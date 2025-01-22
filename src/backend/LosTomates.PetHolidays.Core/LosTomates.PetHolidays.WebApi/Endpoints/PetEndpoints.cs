using LosTomates.PetHolidays.Core.Application.Pets;

namespace LosTomates.PetHolidays.Core.WebApi.Endpoints;

public static class PetEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/pets")
                          .WithTags("Pets management")
                          .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IPetService service, string userId) => await service.GetByUserId(userId))
                .WithSummary("Get user's pets");

        mapGroup.MapGet("{petId}", async (IPetService service, int petId) => await service.GetById(petId))
                .WithSummary("Get pet by id");

        mapGroup.MapPost(string.Empty, async (IPetService service, string userId, PetEditDto dto) => await service.Create(userId,dto))
                .WithSummary("Create new pet for user");

        mapGroup.MapPut("{petId}", async (IPetService service, int petId, PetEditDto dto) => await service.Update(petId, dto))
                .WithSummary("Update pet");

        mapGroup.MapDelete("{petId}", async (IPetService service, int petId) => await service.Delete(petId))
                .WithSummary("Delete pet");
    }
}