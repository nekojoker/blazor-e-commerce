using System.Net.Http.Json;
using BlazorEC.Client.Extensions;
using BlazorEC.Client.Util;
using BlazorEC.Shared.Entities;

namespace BlazorEC.Client.Services;

public interface IPublicReviewService
{
    ValueTask<Review> GetAsync(int id);
    ValueTask<List<Review>> FilterByProductIdAsync(int productId);
}

public class PublicReviewService : IPublicReviewService
{
    private readonly HttpClient httpClient;

    public PublicReviewService(PublicHttpClient publicHttpClient)
        => this.httpClient = publicHttpClient.Http;

    public async ValueTask<List<Review>> FilterByProductIdAsync(int productId)
    {
        var response = await httpClient.GetAsync($"api/review/filter/{productId}");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<List<Review>>();
    }

    public async ValueTask<Review> GetAsync(int id)
    {
        var response = await httpClient.GetAsync($"api/review/{id}");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<Review>();
    }

}

