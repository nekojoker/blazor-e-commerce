using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BlazorEC.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentsController : Controller
{
    [HttpPost("create-checkout-session")]
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
            SuccessUrl = $"{GetBaseUrl()}/payment-success",
            CancelUrl = $"{GetBaseUrl()}/payment-cancel",
            Metadata = new Dictionary<string, string>
            {
                ["userId"] = GetUserId()
            }
        };

        var service = new SessionService();
        Session session = service.Create(options);
        
        return Ok(session.Url);
    }

    public string GetBaseUrl()
        => $"{Request.Scheme}://{Request.Host}";

    private string GetUserId()
        => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
    
}

