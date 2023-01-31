using BlazorWebTab.Shared;

namespace BlazorWebTab.Client.Services.CartService;

public interface ICartService
{
    event Action OnCartChange;
    Task AddToCart(CarItem cartItem);
    Task<List<CarItem>> GetCartItems();
}