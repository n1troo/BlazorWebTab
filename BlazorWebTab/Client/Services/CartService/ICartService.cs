using BlazorWebTab.Shared;
using BlazorWebTab.Shared.DTOs;

namespace BlazorWebTab.Client.Services.CartService;

public interface ICartService
{
    event Action OnCartChange;
    Task AddToCart(CarItem cartItem);
    Task<List<CarItem>> GetCartItems();
    Task<List<CartProductResponceDto>> GetCartProducts();
    Task RemoveProductFromCart(int productId, int productTypeId);

}