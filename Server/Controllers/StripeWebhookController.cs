using System;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BlazorEC.Server.Controllers;

[Route("api/[controller]")]
[ApiExplorerSettings(IgnoreApi = true)]
public class StripeWebhookController : ControllerBase
{
    private readonly IOrderService orderService;
    private readonly IConfiguration configuration;

    public StripeWebhookController(IOrderService orderService, IConfiguration configuration)
    {
        this.orderService = orderService;
        this.configuration = configuration;
    }

    [HttpPost]
    [Route("fulfill")]
    public async ValueTask<IActionResult> FulFill()
    {
        var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
              json,
              Request.Headers["Stripe-Signature"],
              configuration["Stripe:WebhookSecret"]
            );

            if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Stripe.Checkout.Session;
                await orderService.InsertAsync(session);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e.Message);
        }
    }

}

