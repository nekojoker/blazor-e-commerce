using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BlazorEC.Shared.Entities;
using BlazorEC.Server.Services;

namespace BlazorEC.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductService productService;

    public ProductController(IProductService productService)
        => this.productService = productService;    

    [HttpGet("{id}")]
    public async ValueTask<ActionResult<Product>> Get(int id)
        => Ok(await productService.GetAsync(id));

    [HttpGet]
    public async ValueTask<ActionResult<List<Product>>> GetAll()
        => Ok(await productService.GetAllAsync());

    [HttpGet("filter/ids")]
    public async ValueTask<ActionResult<List<Product>>> FilterAllByIds([FromQuery] int[] ids)
        => Ok(await productService.FilterAllByIdsAsync(ids));

}

