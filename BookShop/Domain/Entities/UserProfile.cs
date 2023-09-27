using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Domain.Entities;

public class UserProfile
{
    [DisplayName("Имя")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(20)]
    public required string FirstName { get; set; }

    [DisplayName("Фамилия")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(30)]
    public required string LastName { get; set;}

    [DisplayName("Email пользователя")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(50)]
    public required string Email { get; set;}

    [DisplayName("Номер телефона")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(30)]
    public required string PhoneNumber { get; set;}

    [DisplayName("Почтовый индекс")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(10)]
    public required string PostalCode { get; set;}

    [DisplayName("Страна")]
    [StringLength(25)]
    public required string Country { get; set;}

    [DisplayName("Адресс доставки")]
    [Required(ErrorMessage = "{0} является обязательным полем")]
    [StringLength(200)]
    public required string Address { get; set;}
    public Guid UserId { get; set;}

    public required User User { get; set;}

}
