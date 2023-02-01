 using System.Net.Http.Json;
 using Blazored.LocalStorage;
using BlazorWebTab.Shared;
 using BlazorWebTab.Shared.DTOs;

 namespace BlazorWebTab.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    public event Action? OnCartChange;

    public CartService(ILocalStorageService localStorageService, HttpClient httpClient)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
    }

    public async Task AddToCart(CarItem cartItem)
    {
        var cart = await CartItems();
        if (cart != null)
        {
            cart.Add(cartItem);
            await _localStorageService.SetItemAsync<List<CarItem>>("cart", cart);
        }
        
        OnCartChange.Invoke();;
    }

    public async Task<List<CarItem>> GetCartItems()
    {
        var cart = await CartItems();
        return cart;
    }

    public async Task<List<CartProductResponceDto>> GetCartProducts()
    {
        var cartitems = await _localStorageService.GetItemAsync<List<CarItem>>("cart");
        var responseMessage = await _httpClient.PostAsJsonAsync($"api/cart/products", cartitems);
        var carProducts = responseMessage.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponceDto>>>();

        return carProducts.Result.Data;
    }

    public async Task RemoveProductFromCart(int productId, int productTypeId)
    {
        var cartItems = await _localStorageService.GetItemAsync<List<CarItem>>("cart");
        var itemsToRemove = cartItems.Where(ci => ci.ProductId == productId && ci.ProductTypeId == productTypeId).ToList();
        
        cartItems.RemoveAll(s => itemsToRemove.Contains(s));

        await _localStorageService.SetItemAsync("cart", cartItems);

        // await _localStorageService.ClearAsync();
        //
        // foreach (var item in cartItems)
        // {
        //     AddToCart(item);
        // }
        
        OnCartChange.Invoke();

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