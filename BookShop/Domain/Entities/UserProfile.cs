namespace Domain.Entities;

public class UserProfile
{
    public required string FirstName { get; set; }
    public required string LastName { get; set;}
    public required string Email { get; set;}
    public required string PhoneNumber { get; set;}
    public required string PostalCode { get; set;}
    public required string Country { get; set;}
    public required string Address { get; set;}

}
