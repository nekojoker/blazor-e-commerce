using System;
using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Extensions;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEC.Server.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly GraphServiceClient graphClient;

    public UserController(IConfiguration configuration)
    {
        var scopes = new[] { "https://graph.microsoft.com/.default" };

        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        var clientSecretCredential = new ClientSecretCredential(
            configuration["AzureAdB2C:TenantId"],
            configuration["AzureAdB2C:ClientId"],
            configuration["AzureAdB2C:ClientSecret"],
            options);

        graphClient = new GraphServiceClient(clientSecretCredential, scopes);
    }

    [HttpGet("me")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<ActionResult<ShopUser>> GetMe()
    {
        string userId = GetUserId();
        var user = await graphClient.Users[userId].Request().GetAsync();

        return Ok(user.ToShopUser());
    }

    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<IActionResult> Put(ShopUser shopUser)
    {
        string userId = GetUserId();

        var user = new User
        {
            DisplayName = shopUser.DisplayName,
            MobilePhone = shopUser.MobilePhone
        };

        await graphClient.Users[userId].Request().UpdateAsync(user);

        return NoContent();
    }

    private string GetUserId()
        => User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

}

