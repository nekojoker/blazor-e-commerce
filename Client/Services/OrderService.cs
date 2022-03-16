using System;
using System.Net.Http.Json;
using BlazorEC.Shared.Entities;

namespace BlazorEC.Client.Services;

public interface IOrderService
{
    ValueTask<List<Order>> GetAllAsync();
    ValueTask<Order> GetAsync(int orderId);
}

public class OrderService : IOrderService
{
    private readonly HttpClient httpClient;

    public OrderService(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async ValueTask<List<Order>> GetAllAsync()
        => await httpClient.GetFromJsonAsync<List<Order>>("api/order");

    public async ValueTask<Order> GetAsync(int orderId)
        => await httpClient.GetFromJsonAsync<Order>($"api/order/{orderId}");
}

