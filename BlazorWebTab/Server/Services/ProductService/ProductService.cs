using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebTab.Server.Services.ProductService;

public class ProductService : IProductService
{
    private readonly DataContext _dbContext;

    public ProductService(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<ServiceResponse<List<Product>>> GetProducts()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dbContext.Products.ToListAsync(),
            Success = true
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var response = new ServiceResponse<Product>
        {
            Data = await _dbContext.Products.FindAsync(productId),
            Success = true
        };

        return response;
    }

    public async Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl)
    {
        var response = new ServiceResponse<List<Product>>()
        {
            Data = await _dbContext.Products
                .Where(s => s.Category.Url.ToLower() == categoryUrl.ToLower())
                .ToListAsync(),
            Success = true
        };
        return response;
    }
}