using System;
using System.Collections.Specialized;
using System.Net.Http.Json;
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
        => await httpClient.GetFromJsonAsync<List<Review>>($"api/review/filter/{productId}");

    public async ValueTask<Review> GetAsync(int id)
        => await httpClient.GetFromJsonAsync<Review>($"api/review/{id}");

}

