namespace LosTomates.PetHolidays.Core.Exceptions;

public sealed class BusinessLogicException : Exception
{
    public BusinessLogicException()
    {
    }

    public BusinessLogicException(string message) : base(message)
    {
    }

    public BusinessLogicException(IEnumerable<string> messages)
        : base(string.Join(Environment.NewLine, messages))
    {
    }
}
