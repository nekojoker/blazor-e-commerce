using System;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using BlazorEC.Server.Services;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService paymentService;

    public PaymentController(IPaymentService paymentService)
        => this.paymentService = paymentService;
    
    [HttpPost("create-checkout-session")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<ActionResult<string>> CreateCheckoutSession([FromBody] List<Cart> carts)
    {
        var sessionUrl = await paymentService.CreateCheckoutSessionAsync(carts, GetUserId());
        return Ok(sessionUrl);
    }

    private string GetUserId()
        => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
}

