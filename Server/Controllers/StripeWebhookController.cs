using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BlazorEC.Server.Controllers;

[Route("api/[controller]")]
public class StripeWebhookController : Controller
{
    private readonly IOrderService orderService;
    private readonly ILogger<StripeWebhookController> logger;
    private readonly IConfiguration configuration;

    public StripeWebhookController(ILogger<StripeWebhookController> logger,
        IOrderService orderService, IConfiguration configuration)
    {
        this.orderService = orderService;
        this.logger = logger;
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
                
                await FulfillOrder(session);
            }

            return Ok();
        }
        catch (StripeException e)
        {
            return BadRequest(e);
        }
    }

    private async ValueTask FulfillOrder(Stripe.Checkout.Session session)
        => await orderService.InsertAsync(session);  

}

