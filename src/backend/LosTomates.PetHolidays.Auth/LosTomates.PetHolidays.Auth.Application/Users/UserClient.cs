using System.Net.Http.Json;
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

    public async Task CreateAsync(string userId, string userName)
    {
        var response = await _httpClient.PostAsJsonAsync(_coreUserServiceEndpoint, new { UserId = userId, UserName = userName });
        response.EnsureSuccessStatusCode();
    }
}