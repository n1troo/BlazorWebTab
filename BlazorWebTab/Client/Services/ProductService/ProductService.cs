using System.Net.Http.Json;
using BlazorWebTab.Shared;

namespace BlazorWebTab.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public string Message { get; set; } = "Products loading...";
    
    public event Action? ProductChanged;
    public List<Product?> Products { get; set; } = new();
    
    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task GetProducts(string? categoryUrl = null)
    {
        string endpoint;

        if (categoryUrl == null)
            endpoint = "api/Product";
        else
            endpoint = $"api/Product/category/{categoryUrl}";

        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>(endpoint);
            
        if (result != null && result.Data != null) 
                Products = result.Data;
        
        ProductChanged?.Invoke();
    }
    
    public async Task<ServiceResponse<Product>> GetproductById(int productId)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return result;
    }

    public async Task<List<string>> GetProductSearchSuggestions(string searchText)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<string>>>($"api/Product/searchsuggestions/{searchText}");
        return result.Data;
    }

    public async Task SearchProduct(string searchText)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>($"api/Product/search/{searchText}");

        if (result != null)
        {
            Products = result.Data;
        }

        if (Products.Count() == 0)
            Message = "No products found.";
        
        ProductChanged?.Invoke();

    }
}