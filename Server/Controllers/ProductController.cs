using Microsoft.AspNetCore.Mvc;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net.Mime;

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
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async ValueTask<ActionResult<Product>> Get(int id)
    {
        var product = await productService.GetAsync(id);
        if(product is null)
            return NotFound("商品が見つかりませんでした。");

        return Ok(product);
    }

    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<ActionResult<List<Product>>> GetAll()
        => Ok(await productService.GetAllAsync());

    [HttpGet("filter/ids")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async ValueTask<ActionResult<List<Product>>> FilterAllByIds([FromQuery] int[] ids)
        => Ok(await productService.FilterAllByIdsAsync(ids));

}

