namespace Application.Contract;

public record BasketDto
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public List<BookDto> BookDtos { get; init; } = new();
}
