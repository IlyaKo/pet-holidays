using LosTomates.PetHolidays.Core.Application.Users;
using System.Security.Claims;

namespace LosTomates.PetHolidays.Auth.WebApi.Middleware;

public sealed class UserHandlerMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context, ICurrentUserSetter userProvider)
    {
        if (context.User?.Identity?.IsAuthenticated == true)
        {
            var userId = context.User.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            userProvider.Set(userId);
        }

        await _next(context);
    }
}
