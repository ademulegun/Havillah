using Microsoft.AspNetCore.Components;

namespace HavillahWebUI_Server.Pages;

public class RedirectToLogin: ComponentBase
{
    [Inject]
    protected NavigationManager Navigation { get; set; }
    
    protected override void OnInitialized()
    {
        Navigation.NavigateTo("/");
    }
}