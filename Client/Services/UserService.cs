using System;
using System.Net.Http.Json;
using BlazorEC.Shared.Entities;

namespace BlazorEC.Client.Services;

public interface IUserService
{
    ValueTask<ShopUser> GetMeAsync();
    ValueTask<HttpResponseMessage> PutAsync(ShopUser user);
}

public class UserService : IUserService
{
    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async ValueTask<ShopUser> GetMeAsync()
        => await httpClient.GetFromJsonAsync<ShopUser>("api/user/me");

    public async ValueTask<HttpResponseMessage> PutAsync(ShopUser user)
        => await httpClient.PutAsJsonAsync($"api/user", user);
}

