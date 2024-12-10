namespace LosTomates.PetHolidays.Tests;

public class RequestContext
{
    private readonly static AsyncLocal<string> TestId = new AsyncLocal<string>();

    public static void SetTestId(string testId)
    {
        if(string.IsNullOrWhiteSpace(testId))
            throw new ArgumentException();

        if(!string.IsNullOrWhiteSpace(TestId.Value))
            throw new InvalidCastException();

        TestId.Value = testId;
    }

    public static string GetTestId() => TestId.Value;
}

public class Client
{
    private HttpClient httpClient;

    public Client()
    {
        httpClient = new HttpClient();
    }

    public void SetOrUpdateTestId()
    {
        httpClient.DefaultRequestHeaders.Remove(TestContants.RequestId);
        httpClient.DefaultRequestHeaders.Remove(TestContants.TestId);
        httpClient.DefaultRequestHeaders.Add(TestContants.RequestId, Guid.NewGuid().ToString());
        httpClient.DefaultRequestHeaders.Add(TestContants.TestId, RequestContext.GetTestId());
    }
}