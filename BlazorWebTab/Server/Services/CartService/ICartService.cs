using BlazorWebTab.Shared;
using BlazorWebTab.Shared.DTOs;

namespace BlazorWebTab.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponceDto>>> GetCartProducts(List<CarItem> cartItems);
}