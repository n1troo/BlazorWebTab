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
            Data = await _dbContext.Products.ToListAsync()
        };

        return response;
    }

    public async Task<ServiceResponse<Product>> GetProduct(int productId)
    {
        var product = await _dbContext.Products.FindAsync(productId);
        var response = new ServiceResponse<Product>();

        if (product == null)
        {
            response.Message = "There is no product";
            response.Success = false;
        }
        else
        {
            response.Success = true;
            response.Data = product;
        }

        return response;
    }
}