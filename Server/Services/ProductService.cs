
using System;
using System.Linq;
using BlazorEC.Server.Data;
using BlazorEC.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorEC.Server.Services;

public interface IProductService
{
    ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids);
    ValueTask<List<Product>> GetAllAsync();
    ValueTask<Product> GetAsync(int id);
}

public class ProductService : IProductService
{
    private readonly DataContext context;

    public ProductService(IDbContextFactory<DataContext> factory)
        => context = factory.CreateDbContext();

    public async ValueTask<Product> GetAsync(int id)
    {
        using (context)
        {
            return await context.Products.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

    public async ValueTask<List<Product>> GetAllAsync()
    {
        using (context)
        {
            return await context.Products.ToListAsync();
        }
    }

    public async ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids)
    {
        using (context)
        {
            return await context.Products.Where(x => ids.Contains(x.Id)).ToListAsync();
        }
    }
}

