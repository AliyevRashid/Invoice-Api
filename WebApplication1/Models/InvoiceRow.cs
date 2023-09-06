using Microsoft.AspNetCore.Server.Kestrel.Core.Features;

namespace Invoice_Api.Models;

public class InvoiceRow
{
    public int Id { get; set; }
    public int InnvoiceId { get; set; }
    public string Service { get; set; }
    public decimal Quantity { get; set; }
    public decimal Rate { get; set; }
    public decimal Sum { get; set; } 
}
