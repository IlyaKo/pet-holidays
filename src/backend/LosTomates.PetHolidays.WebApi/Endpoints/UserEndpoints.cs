using LosTomates.PetHolidays.Application.Users;

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
        .Produces<UserView>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest);

        mapGroup.MapPut("{userId}", async (IUserService service, string userId, UserEditDto dto) =>
        {
            await service.Update(userId, dto);

        })
        .WithSummary("Update a user record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);
    }
}
