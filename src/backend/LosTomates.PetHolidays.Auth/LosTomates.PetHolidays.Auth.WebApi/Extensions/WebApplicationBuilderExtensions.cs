using LosTomates.PetHolidays.Auth.WebApi.Endpoints;

namespace LosTomates.PetHolidays.Auth.WebApi.Extensions;

internal static class WebApplicationBuilderExtensions
{
    internal static IApplicationBuilder MapApplicationEndpoints(this WebApplication app)
    {
        UserEndpoints.Map(app);

        return app;
    }
}
