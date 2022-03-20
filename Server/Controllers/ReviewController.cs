using System;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEC.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewController(IReviewService reviewService)
        => this.reviewService = reviewService;

    [AllowAnonymous]
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<ActionResult<Review>> Get(int id)
        => Ok(await reviewService.GetAsync(id));

    [AllowAnonymous]
    [HttpGet("filter/{productId}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<ActionResult<List<Review>>> FilterByProductIdAsync(int productId)
        => Ok(await reviewService.FilterByProductIdAsync(productId));

    [Authorize]
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async ValueTask<ActionResult<int>> Post(Review review)
        => Ok(await reviewService.PostAsync(review, GetUserId()));

    [Authorize]
    [HttpPut]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<ActionResult> Put(Review review)
    {
        await reviewService.PutAsync(review, GetUserId());
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<ActionResult> Delete(int id)
    {
        await reviewService.DeleteAsync(id, GetUserId());
        return NoContent();
    }

    private Guid GetUserId()
        => Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
}

