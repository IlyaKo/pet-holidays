using LosTomates.PetHolidays.Core.WebApi.Endpoints;

namespace LosTomates.PetHolidays.Core.WebApi.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static IApplicationBuilder MapApplicationEndpoints(this WebApplication app)
    {
        AdminEndpoints.Map(app);
        HotelEndpoints.Map(app);
        RoomEndpoints.Map(app);
        RoomTypeEndpoints.Map(app);
        BookingEndPoints.Map(app);
        PetTypeEndpoints.Map(app);
        PetEndpoints.Map(app);

        return app;
    }
}
