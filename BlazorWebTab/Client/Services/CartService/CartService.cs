 using Blazored.LocalStorage;
using BlazorWebTab.Shared;

namespace BlazorWebTab.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorageService;
    public event Action? OnChange;

    public CartService(ILocalStorageService localStorageService)
    {
        _localStorageService = localStorageService;
    }

    public async Task AddToCart(CarItem cartItem)
    {
        var cart = await CartItems();
        if (cart != null)
        {
            cart.Add(cartItem);
            await _localStorageService.SetItemAsync<List<CarItem>>("cart", cart);
        }
    }

    public async Task<List<CarItem>> GetCartItems()
    {
        var cart = await CartItems();
        return cart;
    }

    private async Task<List<CarItem>?> CartItems()
    {
        var cart = await _localStorageService.GetItemAsync<List<CarItem>>("cart");
        if (cart == null)
        {
            cart = new List<CarItem>();
        }
        return cart;
    }
}