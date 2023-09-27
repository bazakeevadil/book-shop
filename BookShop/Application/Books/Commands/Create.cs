using MediatR;

namespace Application.Books.Commands;

public record CreateBookResponse
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public required string AuthorName { get; set; }
    public Guid AuthorId { get; set; }
    public decimal Price { get; set; }
}
public record CreateBookCommand : IRequest<CreateBookResponse>
{
    public required string Title { get; init; }
    public string? Description { get; init; }
    public required string AuthorName { get; set; }
    public decimal Price { get; init; }
}
