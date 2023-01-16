using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Blazored.LocalStorage;
using Havillah.ApplicationServices.Authentication.Dto;
using Havillah.Shared;
using HavillahWebUI_Server.Contracts;
using HavillahWebUI_Server.Middleware;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using LoginDto = Havillah.Shared.LoginDto;
namespace HavillahWebUI_Server.Data;

public class AuthenticationService: IAuthenticationService
{
    private readonly IHttpClientFactory _httpClient;
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly ILocalStorageService _sessionStorageService;
    public AuthenticationService(IHttpClientFactory httpClientInstance, AuthenticationStateProvider authenticationStateProvider,
        ILocalStorageService sessionStorageService)
    {
        _httpClient = httpClientInstance;
        _authenticationStateProvider = authenticationStateProvider;
        _sessionStorageService = sessionStorageService;
    }

    public async Task<AuthenticationResult> Login(LoginDto loginModel)
    {
        var client = _httpClient.CreateClient("authenticationClient");
        var loginAsJson = JsonSerializer.Serialize(loginModel);
        var response = await client.PostAsync("/security/generateToken", new StringContent(loginAsJson, Encoding.UTF8, "application/json"));
        var loginResult = JsonSerializer.Deserialize<AuthenticationResult>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

        if (!response.IsSuccessStatusCode)
        {
            return loginResult;
        }
        await _sessionStorageService.SetItemAsync("authToken", loginResult?.Token);
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginModel.Email);
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult?.Token);
        return loginResult;
    }

    public async Task Logout()
    {
        var client = _httpClient.CreateClient("authenticationClient");
        await _sessionStorageService.RemoveItemAsync("authToken");
        ((CustomAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
        //client.Authorization = null;
        client.DefaultRequestHeaders.Authorization = null;
    }
}