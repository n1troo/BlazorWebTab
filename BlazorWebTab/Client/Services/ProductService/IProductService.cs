using BlazorWebTab.Shared;
using System.Net.Http;

namespace BlazorWebTab.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductChanged;
        List<Product?> Products { get; set; }
        string Message { get; set; }

        int CurrentPage { get; set; }
        int PageCount { get; set; }
        
        string LastSearchText { get; set; }
        
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetproductById(int productId);
        
        Task SearchProduct(string searchText, int page);
        Task<List<string>> GetProductSearchSuggestions(string searchText);
        
    }
}
