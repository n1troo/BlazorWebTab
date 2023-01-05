using BlazorWebTab.Shared;
using System.Net.Http;

namespace BlazorWebTab.Client.Services.ProductService
{
    public interface IProductService
    {
        List<Product?> Products { get; set; }
        Task GetProducts();
        Task<ServiceResponse<Product>> GetproductById(int productId);
    }
}
