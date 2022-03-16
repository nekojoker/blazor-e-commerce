using System;
using System.Net.Http.Json;
using BlazorEC.Client.State;
using BlazorEC.Shared.Entities;
using Blazored.LocalStorage;

namespace BlazorEC.Client.Services;

public interface ICartService
{
    ValueTask UpdateAsync(CartStorage cartStorage);
    ValueTask AddAsync(CartStorage cartStorage);
    ValueTask RemoveAsync(int productId);
    ValueTask<List<Cart>> GetAllAsync();
}

public class CartService : ICartService
{
    private const string CART = "cart";

    private readonly ILocalStorageService storageService;
    private readonly IPublicProductService publicProductService;
    private readonly ICartState cartState;

    public CartService(ILocalStorageService storageService, IPublicProductService publicProductService,
        ICartState cartState)
    {
        this.storageService = storageService;
        this.publicProductService = publicProductService;
        this.cartState = cartState;
    }

    private async ValueTask UpdateCartStorage(CartStorage cart, CartStorageType type)
    {
        var storages = await storageService.GetItemAsync<List<CartStorage>>(CART)
                       ?? new List<CartStorage>();

        var storage = storages.Find(x => x.ProductId == cart.ProductId);
        if (storage is null)
        {
            storages.Add(cart);
            await storageService.SetItemAsync(CART, storages);
            await cartState.Update();
            return;
        }

        switch (type)
        {
            case CartStorageType.Add:
                storage.Quantity += cart.Quantity;
                break;
            case CartStorageType.Update:
                storage.Quantity = cart.Quantity;
                break;
            case CartStorageType.Remove:
                storages.Remove(storage);
                break;
            default:
                break;
        }

        await storageService.SetItemAsync(CART, storages);
        await cartState.Update();
    }

    public async ValueTask AddAsync(CartStorage cart)
        => await UpdateCartStorage(cart, CartStorageType.Add);


    public async ValueTask UpdateAsync(CartStorage cart)
        => await UpdateCartStorage(cart, CartStorageType.Update);


    public async ValueTask RemoveAsync(int productId)
        => await UpdateCartStorage(new CartStorage { ProductId = productId }, CartStorageType.Remove);


    public async ValueTask<List<Cart>> GetAllAsync()
    {
        var storages = await storageService.GetItemAsync<List<CartStorage>>(CART);
        if (storages is null)
            return new List<Cart>();

        var products = await publicProductService.FilterAllByIdsAsync(storages.Select(x => x.ProductId).ToArray());

        return products.Select(p => new Cart
        {
            Quantity = storages.FirstOrDefault(s => s.ProductId == p.Id).Quantity,
            Product = p
        }).ToList();
    }
}

