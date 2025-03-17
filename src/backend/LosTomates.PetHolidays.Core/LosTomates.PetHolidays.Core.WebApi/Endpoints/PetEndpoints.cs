using LosTomates.PetHolidays.Core.Application.Hotels;
using LosTomates.PetHolidays.Core.Application.Pets;
using LosTomates.PetHolidays.Core.Application.Users;
using LosTomates.PetHolidays.Core.Core.Domain.Pets;
using LosTomates.PetHolidays.Core.Core.FileService;

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

        mapGroup.MapPost("{petId}/photo", async (int petId, IFormFile photo, IFileServiceClient fileServiceClient, IPetService service, ICurrentUserProvider currentUserProvider) => 
        {
            var userId = currentUserProvider.GetUserId();
            await service.GetById(petId, userId);

            if (photo == null || photo.Length == 0)
                return Results.BadRequest("No photo uploaded");

            var url = await fileServiceClient.UploadFileAsync(photo, petId.ToString(), "pets");

            return Results.Ok(new { Url = url });
        })
        .Accepts<IFormFile>("multipart/form-data")
        .WithSummary("Upload pet photo")
        .DisableAntiforgery()
        .RequireAuthorization();

        mapGroup.MapGet("{petId}/photo", async (int petId, IFileServiceClient fileServiceClient, IPetService service, ICurrentUserProvider currentUserProvider) =>
        {
            var userId = currentUserProvider.GetUserId();
            await service.GetById(petId, userId);

            var photoUrl = await fileServiceClient.GetFileUrlAsync(petId.ToString(), "pets");

            return Results.Ok(new { PhotoUrl = photoUrl });
        })
        .WithSummary("Get pet photo url")
        .RequireAuthorization();

        mapGroup.MapDelete("{petId}/photo", async (int petId, IFileServiceClient fileServiceClient, IPetService service, ICurrentUserProvider currentUserProvider) =>
        {
            var userId = currentUserProvider.GetUserId();
            await service.GetById(petId, userId);

            await fileServiceClient.DeleteFileAsync(petId.ToString(), "pets");
            return Results.Ok();  

        })
        .WithSummary("Delete pet photo")
        .RequireAuthorization();
    }
}