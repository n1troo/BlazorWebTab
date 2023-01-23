using BlazorWebTab.Shared;
using BlazorWebTab.Shared.DTOs;

namespace BlazorWebTab.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProducts();
        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResultDTO>>SearchProduct(string searchText, int requestedPage);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);
        Task<ServiceResponse<List<Product>>> GetFeaturedProducts();

    }
}
