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

    public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
    {
        var response = new ServiceResponse<List<Product>>
        {
            Data = await _dbContext.Products.ToListAsync()
        };

        return response;
    }
}