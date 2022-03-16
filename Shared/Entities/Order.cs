using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorEC.Shared.Entities;

[Index(nameof(UserId))]
public class Order
{
    public int Id { get; set; }

    public Guid UserId { get; set; }

    [Column(TypeName = "nvarchar(27)")]
    public string StripePeymentId { get; set; }

    public int Amount { get; set; }

    public DateTime CreateDate { get; set; }

    public virtual List<OrderParticular> OrderParticulars { get; set; }
}

