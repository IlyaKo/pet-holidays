using LosTomates.PetHolidays.WebApi.Endpoints;

namespace LosTomates.PetHolidays.WebApi.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static IApplicationBuilder MapApplicationEndpoints(this WebApplication app)
    {
        AdminEndpoints.Map(app);
        HotelEndpoints.Map(app);

        return app;
    }
}
