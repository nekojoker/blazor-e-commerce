using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : ControllerBase
{
    [HttpPost("create-checkout-session")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<string> CreateCheckoutSession([FromBody] List<Cart> carts)
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
            SuccessUrl = $"https://localhost:7030/payment-success",
            CancelUrl = $"https://localhost:7030/payment-cancel",
            Metadata = new Dictionary<string, string>
            {
                ["userId"] = GetUserId()
            }
        };

        var service = new SessionService();
        Session session = service.Create(options);

        return Ok(session.Url);
    }

    private string GetUserId()
        => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

}

