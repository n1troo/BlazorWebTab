using System.Net.Http.Json;
using BlazorWebTab.Shared;

namespace BlazorWebTab.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
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
        
        ProductChanged.Invoke();
    }
    

    public async Task<ServiceResponse<Product>> GetproductById(int productId)
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<Product>>($"api/Product/{productId}");
        return result;
    }
}