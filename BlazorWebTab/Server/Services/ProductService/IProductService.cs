using BlazorWebTab.Shared;

namespace BlazorWebTab.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProducts();
        Task<ServiceResponse<Product>> GetProduct(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<List<Product>>> SearchProduct(string searchText);
        Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText);

    }
}
