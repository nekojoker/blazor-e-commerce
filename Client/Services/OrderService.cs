using System;
using System.Net.Http.Json;
using BlazorEC.Client.Extensions;
using BlazorEC.Shared.Entities;

namespace BlazorEC.Client.Services;

public interface IOrderService
{
    ValueTask<List<Order>> GetAllAsync();
}

public class OrderService : IOrderService
{
    private readonly HttpClient httpClient;

    public OrderService(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async ValueTask<List<Order>> GetAllAsync()
    {
        var response = await httpClient.GetAsync("api/order");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<List<Order>>();
    }
}

