using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BlazorEC.Shared.Entities;

[Index(nameof(UserId))]
public class Review
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public Guid UserId { get; set; }

    [Required]
    [Range(1, 5)]
    public int Rating { get; set; } = 5;

    [Required(ErrorMessage = "タイトルは必須項目です。")]
    [MaxLength(50, ErrorMessage = "タイトルは50文字までです。")]
    [Column(TypeName = "nvarchar(50)")]
    public string Title { get; set; }

    [Required(ErrorMessage = "レビュー本文は必須項目です。")]
    [MaxLength(1000, ErrorMessage = "レビュー本文は1000文字までです。")]
    [Column(TypeName = "nvarchar(1000)")]
    public string ReviewText { get; set; }

    public DateTime CreateDate { get; set; }

    public DateTime UpdateDate { get; set; }

    //public virtual Product Product { get; set; }
}