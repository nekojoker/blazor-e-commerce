using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorEC.Client;
using BlazorEC.Client.Services;
using BlazorEC.Client.Util;
using Blazored.LocalStorage;
using BlazorEC.Client.State;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddHttpClient("BlazorEC.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

builder.Services.AddHttpClient<PublicHttpClient>("BlazorEC.AnonymousAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress));

// Supply HttpClient instances that include access tokens when making requests to the server project
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("BlazorEC.ServerAPI"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPublicProductService, PublicProductService>();
builder.Services.AddScoped<ICartService, CartService>();
builder.Services.AddScoped<ICartState, CartState>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IPublicReviewService, PublicReviewService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddMsalAuthentication(options =>
{
    builder.Configuration.Bind("AzureAdB2C", options.ProviderOptions.Authentication);
    var defaultScope = builder.Configuration["AzureAdB2C:DefaultScope"];
    options.ProviderOptions.DefaultAccessTokenScopes.Add(defaultScope);
});


builder.Services.AddBlazoredLocalStorage();

await builder.Build().RunAsync();
