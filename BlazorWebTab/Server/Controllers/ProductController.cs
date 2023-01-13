using BlazorWebTab.Server.Data;
using BlazorWebTab.Server.Services.ProductService;
using BlazorWebTab.Shared;
using Microsoft.AspNetCore.Mvc;


namespace BlazorWebTab.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }
 
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProducts()
    {
        var result = await _productService.GetProducts();
        return Ok(result);
    }

    [HttpGet("{productId}")]
    public async Task<ActionResult<ServiceResponse<Product>>> GetProduct(int productId)
    {
        var result = await _productService.GetProduct(productId);
        return Ok(result);
    }
    
    [HttpGet("category/{categoryUrl}")]
    public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory(string categoryUrl)
    {
        var result = await _productService.GetProductsByCategory(categoryUrl);
        return Ok(result);
    }

    [HttpGet("search/{searchText}")]
    public async Task<ActionResult<ServiceResponse<Product>>> SearchProducts(string searchText)
    {
        var result = await _productService.SearchProduct(searchText);
        return Ok(result);
    }

    [HttpGet("searchsuggestions/{searchText}")]
    public async Task<ServiceResponse<List<string>>> GetSearchSuggestions(string searchText)
    {
        return await _productService.GetProductSearchSuggestions(searchText);
    }
}