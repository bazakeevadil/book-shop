using Domain.Enums;
using System.Buffers.Text;

namespace Domain.Entities;

public class User
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public Role Role { get; set; }
    public Basket Basket { get; set; } = new();

}
