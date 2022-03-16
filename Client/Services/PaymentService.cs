using System;
using System.Net.Http.Json;
using BlazorEC.Client.State;
using BlazorEC.Shared.Entities;
using Blazored.LocalStorage;

namespace BlazorEC.Client.Services;

public interface IPaymentService
{
    ValueTask<string> GetCheckoutUrlAsync(List<Cart> carts);
}

public class PaymentService : IPaymentService
{
    private readonly HttpClient httpClient;

    public PaymentService(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async ValueTask<string> GetCheckoutUrlAsync(List<Cart> carts)
    {
        var response = await httpClient.PostAsJsonAsync<List<Cart>>("api/payments/create-checkout-session", carts);
        return await response.Content.ReadAsStringAsync();
    }
}

