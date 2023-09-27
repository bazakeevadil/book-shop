namespace Application.Contract;

public record UserProfileDto
{
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string PhoneNumber { get; init; }
    public required string PostalCode { get; init; }
    public required string Country { get; init; }
    public required string Address { get; init; }
    public required Guid UserId { get; init; }

    public required UserDto UserDto { get; init; }
}
