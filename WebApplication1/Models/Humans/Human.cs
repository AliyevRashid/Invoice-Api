using Microsoft.AspNetCore.Identity;

namespace Invoice_Api.Models.Humans;

public abstract class Human:IdentityUser
{
    //public int Id { get; set; }
    //public string Name { get; set; }
    //public string Email { get; set; }
    //public string Passowrd { get; set; }
    //public string PhoneNumber { get; set; }
    public string Adress { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }
}
