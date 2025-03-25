using LosTomates.PetHolidays.Auth.Application.Users;

namespace LosTomates.PetHolidays.Auth.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/auth-users")
                          .WithTags("User management")
                          .WithOpenApi();

        mapGroup.MapPost(string.Empty, async (IUserService service, UserClient userClient, UserEditDto dto) =>
        {
            var result = await service.Create(dto);
            await userClient.CreateAsync(result.Token, dto);
            return Results.Ok(result);
        })
        .WithSummary("Sign up a new user")
        .WithDescription("Return an id of a signed up user")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        mapGroup.MapPost("login", async (IUserService service, LoginDto dto) =>
        {
            var result = await service.Login(dto);
            return Results.Ok(result);
        })
        .WithSummary("Login")
        .WithDescription("Return an JWT with UserId, UserName")
        .Produces<LoginResponse>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        mapGroup.MapGet("current", async (IUserService service) =>
        {
            var view = await service.GetCurrentUser();
            return Results.Ok(view);
        })
        .WithSummary("Get authenticated user view")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();
    }
}
