using LosTomates.PetHolidays.Application.Users;

namespace LosTomates.PetHolidays.WebApi.Endpoints;

public static class UserEndPoints
{
    public static void Map(WebApplication app)
    {
        var mapGroup = app.MapGroup("api/users")
                          .WithTags("Users management")
                          .WithOpenApi();

        mapGroup.MapGet(string.Empty, async (IUserService service) => await service.GetAll())
                .WithSummary("Get list of users")
                .WithDescription("Return a list with all users")
                .Produces<List<UserView>>(StatusCodes.Status200OK);

        mapGroup.MapGet("{userId:int}", async (IUserService service, int userId) =>
        {
            var entityView = await service.GetById(userId);
            if (entityView is null)
                return Results.NotFound("Can't find a record with the id " + userId);
            else
                return Results.Ok(entityView);
        })
        .WithSummary("Get a user by its Id")
        .WithDescription("Return a user")
        .Produces<int>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapPost(string.Empty, async (IUserService service, UserEditDto dto) => await service.Create(dto))
                .WithSummary("Create a new user")
                .WithDescription("Return an id of a created user")
                .Produces<UserView>(StatusCodes.Status200OK);

        mapGroup.MapPut("{userId:int}", async (IUserService service, int userId, UserEditDto dto)
            => await service.Update(userId, dto))
        .WithSummary("Update a user record")
        .Produces(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

        mapGroup.MapDelete("{hotelId:int}", async (IUserService service, int hotelId) => await service.Delete(hotelId))
                .WithSummary("Delete a user record")
                .Produces(StatusCodes.Status200OK);
    }
}
