namespace LosTomates.PetHolidays.Core.Exceptions;

public sealed class NotFoundException : Exception
{
    public NotFoundException()
    {
    }

    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, string key)
        : base($"Can't find a {name} with the {key}")
    {
    }
}