using BlazorWebTab.Server.Data;
using BlazorWebTab.Shared;
using BlazorWebTab.Shared.DTOs;
using Microsoft.EntityFrameworkCore;

namespace BlazorWebTab.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly DataContext _context;

    public CartService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<CartProductResponceDto>>> GetCartProducts(List<CarItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponceDto>>()
        {
            Data = new List<CartProductResponceDto>(),
            Success = true
        };

        foreach (var cartItem in cartItems)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartItem.ProductId);
            if (product == null)
            {
                continue; //skiping loop when null
            }

            var variant =
                await _context.ProductVariants
                    .Include(v=>v.ProductType)
                    .FirstOrDefaultAsync(v =>
                    v.ProductTypeId == cartItem.ProductTypeId &&
                    v.ProductId == cartItem.ProductId);
            
            if (variant == null)
            {
                continue;
            }

            CartProductResponceDto cartprod = new CartProductResponceDto()
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = variant.Price,
                ProductType = variant.ProductType.Name,
                ProductTypeId = variant.ProductTypeId,
                Quantity = cartItem.Quantity
            };
            
            result.Data.Add(cartprod);
        }
        
        return result;
    }
}