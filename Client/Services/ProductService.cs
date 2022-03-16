using System;
using System.Collections.Specialized;
using System.Net.Http.Json;
using BlazorEC.Client.Util;
using BlazorEC.Shared.Entities;

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
        => await httpClient.GetFromJsonAsync<List<Product>>("api/product");

    public async ValueTask<Product> GetAsync(int id)
        => await httpClient.GetFromJsonAsync<Product>($"api/product/{id}");

    public async ValueTask<List<Product>> FilterAllByIdsAsync(int[] ids)
    {
        var query = System.Web.HttpUtility.ParseQueryString(string.Empty);
        foreach (var id in ids)
        {
            query.Add("ids", id.ToString());
        }

        return await httpClient.GetFromJsonAsync<List<Product>>($"api/product/filter/ids?{query.ToString()}");
    }
}

