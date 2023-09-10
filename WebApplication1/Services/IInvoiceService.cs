using Invoice_Api.DTO;
using Invoice_Api.DTO.Pagination;
using Invoice_Api.Models;
using Invoice_Api.Models.Humans;


namespace Invoice_Api.Services;

public interface IInvoiceService
{
    Task<Invoice> Create_Invoice(InvoiceRow_Request request);
    Task<Invoice> Update_Invoice(int id, string comment);
    Task<Invoice> Delete_Invoice(int id);
    Task<IEnumerable<Invoice>> GetAllInvoices();
   
}
