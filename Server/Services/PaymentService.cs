using BlazorEC.Shared.Entities;
using Stripe.Checkout;

namespace BlazorEC.Server.Services;

public interface IPaymentService
{
    ValueTask<string> CreateCheckoutSessionAsync(List<Cart> carts, string userId);
}

public class PaymentService : IPaymentService
{
    public async ValueTask<string> CreateCheckoutSessionAsync(List<Cart> carts, string userId)
    {
        var lineItems = new List<SessionLineItemOptions>();
        foreach (var cart in carts)
        {
            var option = new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "JPY",
                    UnitAmount = (long)cart.Product.UnitPrice,
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Name = cart.Product.Title,
                        Images = new List<string>
                        {
                            cart.Product.ImageUrl
                        },
                        Metadata = new Dictionary<string, string>
                        {
                            ["productId"] = cart.Product.Id.ToString()
                        }
                    }
                },
                Quantity = cart.Quantity,
            };
            lineItems.Add(option);
        }

        var options = new SessionCreateOptions
        {
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = $"https://localhost:5001/payment-success",
            CancelUrl = $"https://localhost:5001/payment-cancel",
            Metadata = new Dictionary<string, string>
            {
                ["userId"] = userId
            }
        };

        var service = new SessionService();
        Session session = await service.CreateAsync(options);

        return session.Url;
    }


}

