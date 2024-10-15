using Blazor_CMS.Client.Shared.Models.DTOs;
using Microsoft.JSInterop;
using System.Text;
using System.Text.Json;

public class AuthenticationServices
{
    private readonly HttpClient _httpClient;
    private readonly IJSRuntime _jsRuntime;

    public AuthenticationServices(HttpClient httpClient, IJSRuntime jsRuntime)
    {
        _httpClient = httpClient;
        _jsRuntime = jsRuntime;
    }

    public async Task<Dictionary<string, object>> RegisterAsync(RegisterRequest userRequest)
    {
        userRequest.Name ??= string.Empty;
        userRequest.Email ??= string.Empty;
        userRequest.Phone ??= string.Empty;
        userRequest.Password ??= string.Empty;
        userRequest.ConfirmPassword ??= string.Empty;
        userRequest.UserName ??= string.Empty;

        var apiUrl = "/Authentication/register";
        var jsonRequest = JsonSerializer.Serialize(userRequest);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Get the real User-Agent from JavaScript
        var userAgent = await _jsRuntime.InvokeAsync<string>("getUserAgent");

        // Add User-Agent header safely
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        var response = await _httpClient.PostAsync(apiUrl, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("json : " + jsonResponse);
        // Check if response is not empty
        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            throw new Exception("Empty response received from server");
        }

        var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);
        Dictionary<string, object> Responses = new();

        foreach (var pair in generalResponses)
        {
            Responses[pair.Key] = pair.Value;
        }

        return Responses;
    }

    public async Task<Dictionary<string, object>> EmailConfirmationAsync(string userRequest)
    {
        var apiUrl = "/Authentication/EmailVerification";
        var jsonRequest = JsonSerializer.Serialize(userRequest);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
        Console.WriteLine("userr " + userRequest);
        // Get the real User-Agent from JavaScript
        var userAgent = await _jsRuntime.InvokeAsync<string>("getUserAgent");

        // Add User-Agent header safely
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        var response = await _httpClient.PostAsync(apiUrl, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        Console.WriteLine("json : " + jsonResponse.ToString());

        // Check if response is not empty
        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            throw new Exception("Empty response received from server");
        }

        var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);
        Dictionary<string, object> Responses = new();

        foreach (var pair in generalResponses)
        {
            Responses[pair.Key] = pair.Value;
        }

        return Responses;
    }

    public async Task<Dictionary<string, object>> LoginAsync(LogInRequest userRequest)
    {
        var apiUrl = "/Authentication/login";
        var jsonRequest = JsonSerializer.Serialize(userRequest);

        var content = new StringContent(jsonRequest, Encoding.UTF8, "application/json");

        // Get the real User-Agent from JavaScript
        var userAgent = await _jsRuntime.InvokeAsync<string>("getUserAgent");

        // Add User-Agent header safely
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        var response = await _httpClient.PostAsync(apiUrl, content);
        var jsonResponse = await response.Content.ReadAsStringAsync();

        // Check if response is not empty
        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            throw new Exception("Empty response received from server");
        }

        var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);
        Dictionary<string, object> Responses = new();

        foreach (var pair in generalResponses)
        {
            Responses[pair.Key] = pair.Value;
        }

        return Responses;
    }

    public async Task<Dictionary<string, object>> LogOutAsync()
    {
        var apiUrl = "/Authentication/SignOut";

        // Get the real User-Agent from JavaScript
        var userAgent = await _jsRuntime.InvokeAsync<string>("getUserAgent");

        // Add User-Agent header safely
        if (!_httpClient.DefaultRequestHeaders.Contains("User-Agent"))
        {
            _httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        var response = await _httpClient.PostAsync(apiUrl, null);
        var jsonResponse = await response.Content.ReadAsStringAsync();
        Console.WriteLine("json : " + jsonResponse);
        // Check if response is not empty
        if (string.IsNullOrWhiteSpace(jsonResponse))
        {
            throw new Exception("Empty response received from server");
        }

        var generalResponses = JsonSerializer.Deserialize<List<GeneralRespons>>(jsonResponse);
        Dictionary<string, object> Responses = new();

        foreach (var pair in generalResponses)
        {
            Responses[pair.Key] = pair.Value;
        }

        return Responses;
    }



    public async Task AddAuthorizationHeaderAsync()
    {
        var accessToken = await _jsRuntime.InvokeAsync<string>("sessionStorage.getItem", "Token");

        if (!string.IsNullOrEmpty(accessToken))
        {
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);
        }
    }


}
