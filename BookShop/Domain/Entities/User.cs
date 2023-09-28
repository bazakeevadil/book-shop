using Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities;

public class User
{
    public Guid Id { get; set; }

    [DisplayName("Имя пользователя")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(20)]
    public required string Username { get; set; }

    [DisplayName("Захешированный пароль")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(200)]
    public required string HashPassword { get; set; }
    public Role Role { get; set; }
    public Basket Basket { get; set; } = new();
    public List<Order> Orders { get; set; } = new();
}
