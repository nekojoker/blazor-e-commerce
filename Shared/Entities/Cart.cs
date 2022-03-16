using System;
namespace BlazorEC.Shared.Entities;

public class Cart
{
    public int Quantity { get; set; }
    public Product Product { get; init; } = new();

    public decimal CalcAmount()
    {
        return Math.Round(Quantity * Product.UnitPrice, MidpointRounding.AwayFromZero);
    }

    public CartStorage ToCartStorage()
    {
        return new CartStorage
        {
            ProductId = Product.Id,
            Quantity = Quantity
        };
    }
}


