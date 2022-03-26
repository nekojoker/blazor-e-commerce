using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
        => this.orderService = orderService;

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<ActionResult<List<Order>>> GetAll()
        => Ok(await orderService.GetAllAsync(GetUserId()));

    private Guid GetUserId()
        => Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

}

