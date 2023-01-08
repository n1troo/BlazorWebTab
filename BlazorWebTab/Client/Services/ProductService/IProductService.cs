using BlazorWebTab.Shared;
using System.Net.Http;

namespace BlazorWebTab.Client.Services.ProductService
{
    public interface IProductService
    {
        event Action ProductChanged;
        List<Product?> Products { get; set; }
        Task GetProducts(string? categoryUrl = null);
        Task<ServiceResponse<Product>> GetproductById(int productId);
    }
}
