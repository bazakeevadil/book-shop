namespace Application.Contract;

public record BookDto
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required string Author { get; init; }
    public required long Quantity { get; init; }
    public required decimal Price { get; init; }
}
