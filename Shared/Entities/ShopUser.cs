using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorEC.Shared.Entities;

public class ShopUser
{
    public string Id { get; set; }

    [Required(ErrorMessage = "表示名は必須項目です。")]
    [MaxLength(30, ErrorMessage = "表示名は30文字までです。")]
    public string DisplayName { get; set; }

    [Required(ErrorMessage = "電話番号は必須項目です。")]
    [Phone(ErrorMessage = "電話番号の形式が正しくありません。")]
    [MaxLength(20, ErrorMessage = "電話番号は20文字までです。")]
    public string MobilePhone { get; set; }
}