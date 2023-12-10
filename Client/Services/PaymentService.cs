using System.Net.Http.Json;
using BlazorEC.Client.Extensions;
using BlazorEC.Shared.Entities;

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
        var response = await httpClient.PostAsJsonAsync("api/payment/create-checkout-session", carts);
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<string>();
    }
}

