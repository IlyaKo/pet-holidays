using LosTomates.PetHolidays.Core.Application.Hotels;
using LosTomates.PetHolidays.Core.Core.FileService;

namespace LosTomates.PetHolidays.Core.WebApi.Endpoints;

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
            return Results.Ok(entityView);
        })
        .WithSummary("Get a hotel by its Id")
        .WithDescription("Return a hotel including not active ones")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IHotelService service, HotelEditDto dto) =>
        {
            return await service.Create(dto);
        })
        .WithSummary("Create a new hotel")
        .WithDescription("Return an id of a created hotel")
        .Produces<HotelView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{hotelId:int}", async (IHotelService service, int hotelId, HotelEditDto dto) =>
        {
            await service.Update(hotelId, dto);
        })
        .WithSummary("Update a hotel record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{hotelId:int}", async (IHotelService service, int hotelId) =>
        {
            await service.Delete(hotelId);
        })
        .WithSummary("Delete a hotel record")
        .Produces(StatusCodes.Status200OK);

        mapGroup.MapPost("{hotelId}/photo", async (
            int hotelId,
            IFormFile photo,
            IFileServiceClient fileServiceClient,
            IHotelService service) =>
        {
            await service.GetById(hotelId);

            if (photo == null || photo.Length == 0)
                return Results.BadRequest("No photo uploaded");

            var url = await fileServiceClient.UploadFileAsync(photo, hotelId.ToString(), "hotels");

            return Results.Ok(new { Url = url });
        })
        .Accepts<IFormFile>("multipart/form-data")
        .WithSummary("Upload hotel photo")
        .DisableAntiforgery();

        mapGroup.MapGet("{hotelId}/photo", async (
            int hotelId,
            IFileServiceClient fileServiceClient,
            IHotelService service) =>
        {
            var photoUrl = await fileServiceClient.GetFileUrlAsync(hotelId.ToString(), "hotels");

            return Results.Ok(new { PhotoUrl = photoUrl }); 
        })
        .WithSummary("Get hotel photo url");

        mapGroup.MapDelete("/api/hotels/{id}/photo", async (
            int id,
                IFileServiceClient fileServiceClient) =>
        {
            await fileServiceClient.DeleteFileAsync(id.ToString(), "hotels");
            return Results.Ok();

        })
        .WithSummary("Delete hotel photo");
    }
}