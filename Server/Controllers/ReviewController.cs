using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;

namespace BlazorEC.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService reviewService;

    public ReviewController(IReviewService reviewService)
        => this.reviewService = reviewService;

    [HttpGet("{id}")]
    public async ValueTask<ActionResult<Review>> Get(int id)
        => Ok(await reviewService.GetAsync(id));

    [HttpGet("filter/{productId}")]
    public async ValueTask<ActionResult<List<Review>>> FilterByProductIdAsync(int productId)
        => Ok(await reviewService.FilterByProductIdAsync(productId));

    [HttpPost]
    public async ValueTask<ActionResult<int>> Post(Review review)
        => Ok(await reviewService.PostAsync(review, GetUserId()));

    [HttpPut]
    public async ValueTask<ActionResult> Put(Review review)
        => Ok(await reviewService.PutAsync(review, GetUserId()));

    [HttpDelete("{id}")]
    public async ValueTask<ActionResult> Delete(int id)
        => Ok(await reviewService.DeleteAsync(id, GetUserId()));

    private Guid GetUserId()
        => Guid.Parse(User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value);
}

