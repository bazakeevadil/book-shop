using Domain.Enums;

namespace Domain.Entities;

public class User
{
    public required Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required Role Role { get; set; }
    public Basket Basket { get; set; } = new();

}
