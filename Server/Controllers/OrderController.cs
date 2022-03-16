using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class OrderController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrderController(IOrderService orderService)
        => this.orderService = orderService;

    [HttpGet("{orderId}")]
    public async ValueTask<ActionResult<Order>> Get(int orderId)
        => Ok(await orderService.GetAsync(orderId, GetUserId()));

    [HttpGet]
    public async ValueTask<ActionResult<List<Order>>> GetAll()
        => Ok(await orderService.GetAllAsync(GetUserId()));

    private Guid GetUserId()
        => Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);

}

