namespace Invoice_Api.Models.Humans;

public class AppUser : Human
{


    public Customer[] Customers { get; set; }
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }

}
