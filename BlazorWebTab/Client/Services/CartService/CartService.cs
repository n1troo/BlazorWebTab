 using System.Net.Http.Json;
 using Blazored.LocalStorage;
using BlazorWebTab.Shared;
 using BlazorWebTab.Shared.DTOs;

 namespace BlazorWebTab.Client.Services.CartService;

public class CartService : ICartService
{
    private readonly ILocalStorageService _localStorageService;
    private readonly HttpClient _httpClient;
    private readonly ILogger<CartService> _logger;
    public event Action? OnCartChange;

    public CartService(ILocalStorageService localStorageService, HttpClient httpClient, ILogger<CartService> logger)
    {
        _localStorageService = localStorageService;
        _httpClient = httpClient;
        _logger = logger;
    }

    public async Task AddToCart(CarItem cartItem)
    {
        var cart = await CartItems();
        if (cart != null)
        {
            var sameId = cart.Find(x => x.ProductId == cartItem.ProductId && x.ProductTypeId == cartItem.ProductTypeId);
            if (sameId == null)
            {
                cart.Add(cartItem);
            }
            else
            {
                sameId.Quantity += 1;
            }
           
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
        if (cartItems == null) {
            return;
        }
        
        var itemsToRemove = cartItems.Where(ci => ci.ProductId == productId && ci.ProductTypeId == productTypeId).ToList();
        cartItems.RemoveAll(s => itemsToRemove.Contains(s));

        await _localStorageService.SetItemAsync("cart", cartItems);

        OnCartChange.Invoke();

    }

    public async Task UpdateQuentity(CartProductResponceDto product)
    {
        var cart = await _localStorageService.GetItemAsync<List<CarItem>>("cart");
        if (cart == null) {
            return;
        }
        
        var foundItems = cart.Find(ci => ci.ProductId == product.ProductId && ci.ProductTypeId == product.ProductTypeId);
        
        
        if (foundItems != null)
        {
            foundItems.Quantity = product.Quantity;
            await _localStorageService.SetItemAsync("cart", cart);
            OnCartChange.Invoke();
        }

        
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