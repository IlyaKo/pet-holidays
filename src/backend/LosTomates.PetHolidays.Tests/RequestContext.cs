namespace LosTomates.PetHolidays.Tests;

public class RequestContext
{
    private readonly static AsyncLocal<string> TestId = new AsyncLocal<string>();

    public static void SetTestId(string testId)
    {
        if(string.IsNullOrWhiteSpace(testId))
            throw new ArgumentException($"недопустимое значение параметра {nameof(testId)} {testId}");

        if(!string.IsNullOrWhiteSpace(TestId.Value))
            throw new InvalidOperationException("Значение параметра уже присвоено");

        TestId.Value = testId;
    }

    public static string GetTestId() => TestId.Value;
}