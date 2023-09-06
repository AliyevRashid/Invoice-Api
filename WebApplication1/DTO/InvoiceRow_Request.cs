namespace Invoice_Api.DTO
{
    public class InvoiceRow_Request
    {
        public string Service_name { get; set; }
        public decimal Quantity  { get; set; }
        public decimal Rate { get; set; }
    }
}
