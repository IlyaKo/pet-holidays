using LosTomates.PetHolidays.Core.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace LosTomates.PetHolidays.WebApi.Middleware
{
    internal sealed class BusinessLogicExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<BusinessLogicExceptionHandler> _logger;

        public BusinessLogicExceptionHandler(ILogger<BusinessLogicExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            if (exception is not BusinessLogicException businessLogicException)
                return false;

            _logger.LogError(
                businessLogicException,
                "Exception occurred: {Message}",
                businessLogicException.Message);

            var problemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Bad Request",
                Detail = businessLogicException.Message
            };

            httpContext.Response.StatusCode = problemDetails.Status.Value;

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
