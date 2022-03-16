using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlazorEC.Shared.Entities;

public class Product
{
    public int Id { get; set; }

    [Column(TypeName = "nvarchar(255)")]
    public string Title { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    [Column(TypeName = "decimal(10,2)")]
    public decimal UnitPrice { get; set; }
}

