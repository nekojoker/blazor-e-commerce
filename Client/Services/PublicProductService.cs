using System.Net.Http.Json;
using BlazorEC.Client.Util;
using BlazorEC.Shared.Entities;
using BlazorEC.Client.Extensions;

namespace BlazorEC.Client.Services;

public interface IPublicProductService
{
    ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids);
    ValueTask<List<Product>> GetAllAsync();
    ValueTask<Product> GetAsync(int id);
}

public class PublicProductService : IPublicProductService
{
    private readonly HttpClient httpClient;

    public PublicProductService(PublicHttpClient client)
        => this.httpClient = client.Http;

    public async ValueTask<List<Product>> GetAllAsync()
    {
        var response = await httpClient.GetAsync("api/product");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<List<Product>>();
    } 

    public async ValueTask<Product> GetAsync(int id)
    {
        var response = await httpClient.GetAsync($"api/product/{id}");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<Product>();
    }

    public async ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids)
    {
        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
        foreach (var id in ids)
        {
            query.Add("ids", id.ToString());
        }

        var response = await httpClient.GetAsync($"api/product/filter/ids?{query}");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<List<Product>>();
    }
}

