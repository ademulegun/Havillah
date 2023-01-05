namespace HavillahWebUI_MVC.Services;

public class ProductHttpClientService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;
    public ProductHttpClientService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        _httpClient.BaseAddress = new Uri(_configuration.GetSection("ProductApiUrl").Value);
    }
}