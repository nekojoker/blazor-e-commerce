using BlazorEC.Client.Services;
using BlazorEC.Client.State;

namespace BlazorEC.Client.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPublicProductService, PublicProductService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<ICartState, CartState>();
        services.AddScoped<IPaymentService, PaymentService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<IPublicReviewService, PublicReviewService>();
        services.AddScoped<IReviewService, ReviewService>();
        return services;
    }
}

