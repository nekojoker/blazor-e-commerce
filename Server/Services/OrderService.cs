
using System;
using System.Linq;
using BlazorEC.Server.Data;
using BlazorEC.Shared.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorEC.Server.Services;

public interface IOrderService
{
    ValueTask<List<Order>> GetAllAsync(Guid userId);
    ValueTask<Order> GetAsync(int orderId, Guid userId);
    ValueTask InsertAsync(Stripe.Checkout.Session session);
}

public class OrderService : IOrderService
{
    private readonly DataContext context;

    public OrderService(IDbContextFactory<DataContext> factory)
        => context = factory.CreateDbContext();

    public async ValueTask<Order> GetAsync(int orderId, Guid userId)
    {
        using (context)
        {
            return await context.Orders
                .Include(x => x.OrderParticulars)
                .ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(x => x.Id == orderId && x.UserId == userId);
        }
    }

    public async ValueTask<List<Order>> GetAllAsync(Guid userId)
    {
        using (context)
        {
            return await context.Orders
                .Include(x => x.OrderParticulars)
                .ThenInclude(p => p.Product)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.CreateDate)
                .ToListAsync();
        }
    }

    public async ValueTask InsertAsync(Stripe.Checkout.Session session)
    {
        using (context)
        {
            var now = DateTime.Now;
            var order = new Order
            {
                CreateDate = now,
                Amount = (int)session.AmountTotal,
                StripePeymentId = session.PaymentIntentId,
                OrderParticulars = new List<OrderParticular>()
            };

            if (session.Metadata.TryGetValue("userId", out string userId))
            {
                order.UserId = Guid.Parse(userId);
            }

            var sessionService = new Stripe.Checkout.SessionService();
            var lineItems = sessionService.ListLineItems(session.Id);
            var stripeProductService = new Stripe.ProductService();

            foreach (var lineItem in lineItems)
            {
                var particular = new OrderParticular
                {
                    CreateDate = now,
                    UserId = order.UserId,
                    Quantity = (int)lineItem.Quantity,
                    UnitPrice = (int)lineItem.Price.UnitAmount,
                };

                var stripeProduct = stripeProductService.Get(lineItem.Price.ProductId);
                if (stripeProduct.Metadata.TryGetValue("productId", out string productId))
                {
                    particular.ProductId = int.Parse(productId);
                }

                order.OrderParticulars.Add(particular);
            }

            context.Orders.Add(order);
            await context.SaveChangesAsync();
        }
    }
}

