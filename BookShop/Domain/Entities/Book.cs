namespace Domain.Entities;

public class Book
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string AuthorName { get; set; }
    public Guid AuthorId { get; set; }
    public decimal Price { get; set; }
}
