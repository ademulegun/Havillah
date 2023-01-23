using HavillahWebUI_Server.Data;
using Microsoft.AspNetCore.Components;

namespace HavillahWebUI_Server.Pages.Product;

public partial class Products
{
    private readonly ProductService productService = null!;
    [Inject] 
    public NavigationManager? NavigationManager { get; set; }
    
    private async Task Authenticate()
    {
        NavigationManager?.NavigateTo("/", true);
    }
}