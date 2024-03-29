using Havillah.Shared;
using HavillahWebUI_Server.Contracts;
using Microsoft.AspNetCore.Components;

namespace HavillahWebUI_Server.Pages.Auhentication;

public partial class Login
{
    private readonly LoginDto _loginDto = new LoginDto();
    [Inject] 
    private IAuthenticationService AuthenticationService { get; set; } = null!;
    [Inject] 
    public NavigationManager? NavigationManager { get; set; }
    
    
    private async Task Authenticate()
    {
        var login = new LoginDto() { Email = _loginDto.Email, Password = _loginDto.Password };
        var loginResult = await AuthenticationService.Login(login);
        if (!string.IsNullOrEmpty(loginResult.Token))
        { 
            NavigationManager?.NavigateTo("/products", true);
        }
        NavigationManager?.NavigateTo("/products", true);
    }
}