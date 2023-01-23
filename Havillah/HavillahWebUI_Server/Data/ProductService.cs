using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Havillah.Shared.Category;
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

    public async Task<Response<string>> AddProducts(AddProductDto product)
    {
        var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        var httpResponse = await _httpClient.PostAsync("product", content);
        if (!httpResponse.IsSuccessStatusCode) return new Response<string>();
        var response = await httpResponse.Content.ReadAsStringAsync();
        var deserializedResponse = JsonSerializer.Deserialize<Response<string>>(response);
        return deserializedResponse ?? new Response<string>();
    }
    
    public async Task<Response<string>> UpdateProducts(ProductToEditViewModel product)
    {
        var content = new StringContent(JsonSerializer.Serialize(product), Encoding.UTF8, "application/json");
        var httpResponse = await _httpClient.PostAsync("updateProduct", content);
        if (!httpResponse.IsSuccessStatusCode) return new Response<string>();
        var response = await httpResponse.Content.ReadAsStringAsync();
        var deserializedResponse = JsonSerializer.Deserialize<Response<string>>(response);
        return deserializedResponse ?? new Response<string>();
    }

    public async Task<ProductsRoot?> GetProducts()
    {
        var products = new ProductsRoot();
        var productsResponse = await _httpClient.GetAsync("products");
        if (!productsResponse.IsSuccessStatusCode) return products;
        var serializedResponse = await productsResponse.Content.ReadAsStringAsync();
        products = JsonSerializer.Deserialize<ProductsRoot>(serializedResponse);
        return products;
    }
    
    public async Task<ProductRoot?> GetProduct(Guid id)
    {
        var product = new ProductRoot();
        var productsResponse = await _httpClient.GetAsync($"product/{id}");
        if (!productsResponse.IsSuccessStatusCode) return product;
        var serializedResponse = await productsResponse.Content.ReadAsStringAsync();
        product = JsonSerializer.Deserialize<ProductRoot>(serializedResponse);
        return product;
    }
    
    public async Task<Response<string>> AddCategory(ProductCategory category)
    {
        var content = new StringContent(JsonSerializer.Serialize(category), Encoding.UTF8, "application/json");
        var httpResponse = await _httpClient.PostAsync("category", content);
        if (!httpResponse.IsSuccessStatusCode) return new Response<string>();
        var response = await httpResponse.Content.ReadAsStringAsync();
        var deserializedResponse = JsonSerializer.Deserialize<Response<string>>(response);
        return deserializedResponse ?? new Response<string>();
    }
    
    public async Task<GetCategories.CategoriesResult?> GetCategories()
    {
        var productsResponse = await _httpClient.GetAsync("categories");
        if (!productsResponse.IsSuccessStatusCode) return new Models.Product.GetCategories.CategoriesResult();
        var serializedResponse = await productsResponse.Content.ReadAsStringAsync();
        var categories = JsonSerializer.Deserialize<GetCategories.CategoriesResult>(serializedResponse);
        return categories;
    }
}