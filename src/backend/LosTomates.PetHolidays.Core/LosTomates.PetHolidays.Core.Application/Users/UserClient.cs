using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Identity;

namespace LosTomates.PetHolidays.Core.Application.Users;

public class UserClient(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<IdentityResult> CreateAsync(UserEditDto dto)
    {
        string json = JsonSerializer.Serialize(dto);
        var inputContent = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync($"http://localhost:8082/api/auth-users/", inputContent);
        response.EnsureSuccessStatusCode();
        var outputContent = await response.Content.ReadAsStringAsync();

        return IdentityResult.Success;
    }
}