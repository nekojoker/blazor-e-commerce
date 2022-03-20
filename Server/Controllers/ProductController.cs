using System;
using Microsoft.AspNetCore.Mvc;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Authorization;

namespace BlazorEC.Server.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
        => this.productService = productService;    

    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async ValueTask<ActionResult<Product>> Get(int id)
        => Ok(await productService.GetAsync(id));

    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async ValueTask<ActionResult<List<Product>>> GetAll()
        => Ok(await productService.GetAllAsync());

    [HttpGet("filter/ids")]
    [Produces("application/json")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Get))]
    public async ValueTask<ActionResult<List<Product>>> FilterAllByIds([FromQuery] int[] ids)
        => Ok(await productService.FilterAllByIdsAsync(ids));

}

