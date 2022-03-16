using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorEC.Shared.Entities;

public class ShopUser
{
    public string Id { get; set; }

    [Required]
    public string DisplayName { get; set; }

    [Required]
    public string MobilePhone { get; set; }
}

