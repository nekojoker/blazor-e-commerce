using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;
using BlazorEC.Server.Services;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService userService;

    public UserController(IUserService userService)
        => this.userService = userService;

    [HttpGet("me")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<ActionResult<ShopUser>> GetMe()
    {
        var shopUser = await userService.GetAsync(GetUserId());
        return Ok(shopUser);
    }

    [HttpPut]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<ActionResult> Put(ShopUser shopUser)
    {
        await userService.PutAsync(shopUser, GetUserId());
        return NoContent();
    }

    private string GetUserId()
        => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

}

