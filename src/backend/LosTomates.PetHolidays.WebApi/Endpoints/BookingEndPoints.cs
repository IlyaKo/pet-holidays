namespace LosTomates.PetHolidays.Application.Bookings;

public class BookingEndPoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/bookings")
                          .WithTags("Hotel bookings")
                          .WithOpenApi();

        mapGroup.MapGet("{userId}", async (IBookingService service, string userId) => await service.GetByUserId(userId))
                .WithSummary("Get list bookings of user")
                .WithDescription("Return a list with all active bookings of user")
                .Produces<List<IReadOnlyList<BookingView>>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{bookingId:int}", async (IBookingService service, int bookingId) =>
        {
            var entityView = await service.GetById(bookingId);
            return Results.Ok(entityView);
        })
        .WithSummary("Get a booking by its Id")
        .WithDescription("Return a booking including not active ones")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IBookingService service, BookingDto dto) =>
        {
            return await service.Create(dto);
        })
        .WithSummary("Create a new booking")
        .WithDescription("Return an id of a created booking")
        .Produces<BookingView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{bookingId:int}", async (IBookingService service, int bookingId, BookingDto dto) =>
        {
            await service.Update(bookingId, dto);
        })
        .WithSummary("Update a booking record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{bookingId:int}", async (IBookingService service, int bookingId) =>
        {
            await service.Delete(bookingId);
        })
        .WithSummary("Delete a booking record")
        .Produces(StatusCodes.Status200OK);
    }
}