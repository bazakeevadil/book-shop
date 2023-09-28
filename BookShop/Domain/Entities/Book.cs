using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities;

public class Book
{
    public Guid Id { get; set; }

    [DisplayName("Название книги")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(30)]
    public required string Title { get; set; }

    [DisplayName("Описание книги")]
    [StringLength(200)]
    public string? Description { get; set; }
    public required string Author { get; set; }
    public required long Quantity { get; set; }
    public decimal Price { get; set; }

    public List<Order> Orders { get; set; } = new();
}
