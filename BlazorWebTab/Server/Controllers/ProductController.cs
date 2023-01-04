using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using Microsoft.AspNetCore.Mvc;


namespace BlazorWebTab.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly DataContext _dbContext;

    public ProductController(DataContext dbContext )
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProduct()
    {
        var products = await _dbContext.Products.ToListAsync();
        return Ok(products);
    }
}