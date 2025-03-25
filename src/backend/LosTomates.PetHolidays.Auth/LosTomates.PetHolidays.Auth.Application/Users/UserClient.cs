using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace LosTomates.PetHolidays.Auth.Application.Users;

public class UserClient
{
    private readonly HttpClient _httpClient;

    private readonly string _coreUserServiceEndpoint;

    public UserClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _coreUserServiceEndpoint = configuration["CoreUserService:Endpoint"] ?? throw new ArgumentException("CoreUserService:Endpoint is not configured", nameof(configuration));
    }

    public async Task CreateAsync(string token, UserEditDto dto)
    {
        string json = JsonSerializer.Serialize(dto);
        var inputContent = new StringContent(json, Encoding.UTF8, "application/json");
        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        var response = await _httpClient.PostAsync(_coreUserServiceEndpoint, inputContent);
        response.EnsureSuccessStatusCode();
    }
}