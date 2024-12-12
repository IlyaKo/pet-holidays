using LosTomates.PetHolidays.Application.Users;
using System.Security.Claims;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class UserEndpoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/users")
                          .WithTags("User management")
                          .WithOpenApi();

        mapGroup.MapGet("{userId}", async (IUserService service, string userId) =>
        {
            var entityView = await service.GetById(userId);
            return Results.Ok(entityView);
        })
        .WithSummary("Get a user by its Id")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IUserService service, UserEditDto dto) =>
        {
            var result = await service.Create(dto);
            return Results.Ok(new { Id = result });
        })
        .WithSummary("Create a new user")
        .WithDescription("Return an id of a created user")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        mapGroup.MapPost("login", async (IUserService service, LoginDto dto) =>
        {
            var result = await service.Login(dto);
            return Results.Ok(new { JWT = result });
        })
        .WithSummary("Login")
        .WithDescription("Return an JWT with UserId, UserName")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized);

        mapGroup.MapGet("profile", async (IUserService service, ClaimsPrincipal user) =>
        {
            var (userId, userName) = await service.CurrentUser(user);
            return Results.Ok(new { UserId = userId, UserName = userName });
        })
        .WithSummary("Get authenticated user profile")
        .WithDescription("Return the UserId and UserName of the authenticated user")
        .Produces<string>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status401Unauthorized)
        .RequireAuthorization();

        mapGroup.MapPut("{userId}", async (IUserService service, string userId, UserEditDto dto) =>
        {
            await service.Update(userId, dto);

        })
        .WithSummary("Update a user record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
