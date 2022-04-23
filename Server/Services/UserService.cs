
using System;
using System.Linq;
using Azure.Identity;
using BlazorEC.Server.Extensions;
using BlazorEC.Shared.Entities;
using Microsoft.Graph;

namespace BlazorEC.Server.Services;

public interface IUserService
{
    ValueTask<ShopUser> GetAsync(string userId);
    ValueTask<int> PutAsync(ShopUser shopUser, string userId);
}

public class UserService : IUserService
{
    private readonly GraphServiceClient graphClient;

    public UserService(IConfiguration configuration)
    {
        var options = new TokenCredentialOptions
        {
            AuthorityHost = AzureAuthorityHosts.AzurePublicCloud
        };

        var clientSecretCredential = new ClientSecretCredential(
            configuration["AzureAdB2C:TenantId"],
            configuration["AzureAdB2C:ClientId"],
            configuration["AzureAdB2C:ClientSecret"],
            options);

        graphClient = new GraphServiceClient(clientSecretCredential);
    }

    public async ValueTask<ShopUser> GetAsync(string userId)
    {
        var user = await graphClient.Users[userId].Request().GetAsync();
        return user.ToShopUser();
    }

    public async ValueTask<int> PutAsync(ShopUser shopUser, string userId)
    {
        var user = new User
        {
            DisplayName = shopUser.DisplayName,
            MobilePhone = shopUser.MobilePhone
        };

        await graphClient.Users[userId].Request().UpdateAsync(user);

        return StatusCodes.Status204NoContent;
    }

}

