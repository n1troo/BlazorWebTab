using System.Net.Http.Json;
using BlazorWebTab.Shared;

namespace BlazorWebTab.Client.Services.ProductService;

public class ProductService : IProductService
{
    private readonly HttpClient _httpClient;
    public List<Product> Products { get; set; }

    public ProductService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task GetProducts()
    {
        var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Product>>>("api/Product");
        if (result != null && result.Data != null)
        {
            Products = result.Data;
        }
    }
}