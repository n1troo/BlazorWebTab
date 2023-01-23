using System.Net.Http.Json;
using BlazorWebTab.Shared;
using BlazorWebTab.Shared.DTOs;

namespace BlazorWebTab.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public string Message { get; set; } = "Products loading...";
    public int CurrentPage { get; set; } = 1;
    public int PageCount { get; set; } = 0;
    public string LastSearchText { get; set; } = string.Empty;

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

        CurrentPage = 1;
        PageCount = 0;

        if (Products.Count == 0)
        {
            Message = "No products fund";
        }
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

    public async Task SearchProduct(string searchText, int page)
    {
        if(searchText != null)
            LastSearchText = searchText;
        
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<ProductSearchResultDTO>>($"api/Product/search/{searchText}/{page}");

        if (result != null)
        {
            Products = result.Data.Products;
            PageCount = PageCount;
            CurrentPage = CurrentPage;
        }

        if (Products.Count() == 0)
            Message = "No products found.";
        
        ProductChanged?.Invoke();

    }
}