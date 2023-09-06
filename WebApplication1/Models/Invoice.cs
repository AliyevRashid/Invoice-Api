namespace Invoice_Api.Models;

public class Invoice
{
    public int Id {get;set;}		
    public int CustomerId { get; set; }	
    public DateTimeOffset StarDate { get; set; }	
    public DateTimeOffset EndDate { get; set; }
    public IEnumerable<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();	
    public decimal TotalSum { get; set; }	
    public string Comment { get; set; }	
    public string Status { get; set; }		
    public DateTimeOffset CreatedAt{get;set;}	
    public DateTimeOffset UpdatetAt{ get; set; } 	
    public DateTimeOffset DeletedAt { get; set; }

   
}
