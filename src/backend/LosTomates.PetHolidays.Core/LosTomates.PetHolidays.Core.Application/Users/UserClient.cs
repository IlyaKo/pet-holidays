namespace LosTomates.PetHolidays.Core.Application.Users;

public class UserClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<bool> CheckExistById(string userId)
    {
        try
        {
            var response = await _httpClient.GetAsync($"http://localhost:8082/api/users/{userId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            return !string.IsNullOrEmpty(content);
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}