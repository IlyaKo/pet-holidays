using LosTomates.PetHolidays.Application.Bookings;
using LosTomates.PetHolidays.WebApi.Endpoints;

namespace LosTomates.PetHolidays.WebApi.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static IApplicationBuilder MapApplicationEndpoints(this WebApplication app)
    {
        AdminEndpoints.Map(app);
        HotelEndpoints.Map(app);
        UserEndpoints.Map(app);
        RoomEndpoints.Map(app);
        RoomTypeEndpoints.Map(app);
        BookingsEndPoints.Map(app);
        PetTypeEndpoints.Map(app);
        PetEndpoints.Map(app);

        return app;
    }
}
