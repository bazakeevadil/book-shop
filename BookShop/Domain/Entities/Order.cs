using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public decimal TotalPrice { get; set; }
    public required string Address { get; set; }
    public OrderStatus OrderStatus { get; set; }
    public User? User { get; set; }
    public List<Book> Books { get; set; } = new();
}
