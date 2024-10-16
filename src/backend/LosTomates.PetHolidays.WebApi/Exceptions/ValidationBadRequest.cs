using System.Runtime.Serialization;

namespace LosTomates.PetHolidays.WebApi;

public class ValidationBadRequest : Exception
{
    public ValidationBadRequest()
    {
    }

    public ValidationBadRequest(FluentValidation.Results.ValidationResult validationResult) : base(validationResult.ToString("~")) // In this case, each message will be separated with a `~`
    {
    }

    public ValidationBadRequest(string? message) : base(message)
    {
    }

    public ValidationBadRequest(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected ValidationBadRequest(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}