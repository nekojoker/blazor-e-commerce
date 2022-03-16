using System;
using System.Collections.Specialized;
using System.Net.Http.Json;
using BlazorEC.Client.Util;
using BlazorEC.Shared.Entities;

namespace BlazorEC.Client.Services;

public interface IReviewService
{
    ValueTask<int> PostAsync(Review review);
    ValueTask<HttpResponseMessage> PutAsync(Review review);
    ValueTask<HttpResponseMessage> DeleteAsync(int id);
}

public class ReviewService : IReviewService
{
    private readonly HttpClient httpClient;

    public ReviewService(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async ValueTask<int> PostAsync(Review review)
    {
        var result = await httpClient.PostAsJsonAsync("api/review", review);
        string id = await result.Content.ReadAsStringAsync();
        return int.Parse(id);
    }

    public async ValueTask<HttpResponseMessage> PutAsync(Review review)
        => await httpClient.PutAsJsonAsync($"api/review", review);

    public async ValueTask<HttpResponseMessage> DeleteAsync(int id)
        => await httpClient.DeleteAsync($"api/review/{id}");
}

