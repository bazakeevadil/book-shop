namespace Domain.Entities;

public class Author
{
    public required Guid Id { get; set; }
    public required string Fullname { get; set; }
    public List<Book> Books { get; set; } = new();
}
