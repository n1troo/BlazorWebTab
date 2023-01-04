using BlazorWebTab.Shared;

namespace BlazorWebTab.Server.Services.ProductService
{
    public interface IProductService
    {
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
    }
}
