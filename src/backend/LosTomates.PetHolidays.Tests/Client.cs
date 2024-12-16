namespace LosTomates.PetHolidays.Tests;

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