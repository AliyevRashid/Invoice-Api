namespace Invoice_Api.Models;

public class Invoice
{
    public int Id {get;set;}		
    public int CustomerId { get; set; }	
    public DateTimeOffset StarDate { get; set; }	
    public DateTimeOffset EndDate { get; set; }
    public IEnumerable<InvoiceRow> Rows { get; set; } = new List<InvoiceRow>();	
    public InvoiceStatus Status { get; set; }
    public decimal TotalSum { get; set; }	
    public string Comment { get; set; }			
    public DateTimeOffset CreatedAt{get;set;}	
    public DateTimeOffset UpdatetAt{ get; set; } 	
    public DateTimeOffset DeletedAt { get; set; }


   
}

public enum InvoiceStatus
{
    Created,
    Sent,
    Received,
    Paid,
    Cancelled,
    Rejected
}