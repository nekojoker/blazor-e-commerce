using System;
using System.Net.Http.Json;
using BlazorEC.Client.Extensions;
using BlazorEC.Shared.Entities;

namespace BlazorEC.Client.Services;

public interface IUserService
{
    ValueTask<ShopUser> GetMeAsync();
    ValueTask PutAsync(ShopUser user);
}

public class UserService : IUserService
{
    private readonly HttpClient httpClient;

    public UserService(HttpClient httpClient)
        => this.httpClient = httpClient;

    public async ValueTask<ShopUser> GetMeAsync()
    {
        var response = await httpClient.GetAsync("api/user/me");
        await response.HandleError();

        return await response.Content.ReadFromJsonAsync<ShopUser>();
    }

    public async ValueTask PutAsync(ShopUser user)
    {
        var response = await httpClient.PutAsJsonAsync($"api/user", user);
        await response.HandleError();
    }
}

