using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEC.Shared.Entities;

public class OrderParticular
{
    public int Id { get; set; }

    public int OrderId { get; set; }

    public Guid UserId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual Product Product { get; set; }
}

