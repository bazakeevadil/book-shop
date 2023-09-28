using Domain.Enums;

namespace Application.Contract;

public record OrderDto
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }
    public required decimal TotalPrice { get; init; }
    public required OrderStatus OrderStatus { get; init; }
}
