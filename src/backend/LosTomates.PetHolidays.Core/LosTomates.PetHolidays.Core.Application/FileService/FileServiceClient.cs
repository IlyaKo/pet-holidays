using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;

namespace LosTomates.PetHolidays.Core.Core.FileService;

public class FileServiceClient : IFileServiceClient
{
    private readonly HttpClient _httpClient;
    private readonly string _fileServiceEndpoint;

    public FileServiceClient(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _fileServiceEndpoint = configuration["FileService:Endpoint"] 
            ?? throw new ArgumentException("FileService:Endpoint is not configured", nameof(configuration));
    }

    public async Task<string> UploadFileAsync(IFormFile file, string entityId, string collectionName)
    {
        if (string.IsNullOrWhiteSpace(collectionName) || string.IsNullOrWhiteSpace(entityId))
            throw new ArgumentException("CollectionName and EntityId must not be empty");
        if (file == null || file.Length == 0)
            throw new ArgumentException("File must not be null or empty");

        using var content = new MultipartFormDataContent();
        using var stream = file.OpenReadStream();
        var fileContent = new StreamContent(stream);
        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
        content.Add(fileContent, "file", file.FileName);

        var response = await _httpClient.PostAsync($"{_fileServiceEndpoint}/upload/{collectionName}/{entityId}", content);
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            throw new HttpRequestException($"Failed to upload file: {response.StatusCode} - {error}");
        }

        var responseData = await response.Content.ReadFromJsonAsync<FileUrlResponse>();
        return responseData?.Url ?? throw new InvalidOperationException("Invalid response format from file service");
    }

    public async Task<string> GetFileUrlAsync(string entityId, string collectionName)
    {
        if (string.IsNullOrWhiteSpace(collectionName) || string.IsNullOrWhiteSpace(entityId))
            throw new ArgumentException("CollectionName and EntityId must not be empty");

        var response = await _httpClient.GetAsync($"{_fileServiceEndpoint}/{collectionName}/{entityId}/url");
        if (!response.IsSuccessStatusCode)
        {
            var error = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                return null;
            throw new HttpRequestException($"Failed to get file URL: {response.StatusCode} - {error}");
        }

        var responseData = await response.Content.ReadFromJsonAsync<FileUrlResponse>();
        return responseData?.Url ?? throw new InvalidOperationException("Invalid response format from file service");
    }

    public async Task DeleteFileAsync(string entityId, string collectionName)
    {
        var response = await _httpClient.DeleteAsync($"{_fileServiceEndpoint}/{collectionName}/{entityId}");
        response.EnsureSuccessStatusCode();
    }

    private class FileUrlResponse
    {
        public required string Url { get; set; }
    }

}
