using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using HavillahWebUI_Server.Models.Product;
using AddProductDto = Havillah.Shared.Product.AddProductDto;

namespace HavillahWebUI_Server.Data;

public class ProductService
{
    private readonly HttpClient _httpClient;
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<AddProductResponse> AddProducts(AddProductDto product)
    {
        var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        var httpResponse = await _httpClient.PostAsync("product", content);
        if (httpResponse.IsSuccessStatusCode)
        {
            var response = await httpResponse.Content.ReadAsStringAsync();
            var deserializedResponse = JsonSerializer.Deserialize<AddProductResponse>(response);
            if (deserializedResponse != null) return deserializedResponse;
        }
        return new AddProductResponse();
    }

    public async Task<ProductsRoot?> GetProducts()
    {
        var products = new ProductsRoot();
        var productsResponse = await _httpClient.GetAsync("products");
        if (productsResponse.IsSuccessStatusCode)
        {
            var serializedResponse = await productsResponse.Content.ReadAsStringAsync();
            products = JsonSerializer.Deserialize<ProductsRoot>(serializedResponse);
        }
        return products;
    }
    
    public async Task<ProductRoot?> GetProduct(Guid id)
    {
        var product = new ProductRoot();
        var productsResponse = await _httpClient.GetAsync($"product/{id}");
        if (productsResponse.IsSuccessStatusCode)
        {
            var serializedResponse = await productsResponse.Content.ReadAsStringAsync();
            product = JsonSerializer.Deserialize<ProductRoot>(serializedResponse);
        }
        return product;
    }
}