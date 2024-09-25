namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class AdminEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/admin")
                          .WithTags("Administation service")
                          .WithOpenApi();

        mapGroup.MapGet("ping", () => "pong")
                .WithSummary("Check backend avaliability")
                .WithDescription("Should return \"pong\" as an answer")
                .Produces(StatusCodes.Status200OK);
    }
}

