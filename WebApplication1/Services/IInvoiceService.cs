using Invoice_Api.DTO;
using Invoice_Api.Models;
using Invoice_Api.Models.Humans;


namespace Invoice_Api.Services;

public interface IInvoiceService
{
    Task<Invoice> Create_Invoice(InvoiceRow_Request request);
    Task<Invoice> Update_Invoice(int id);
    Task<Invoice> Delete_Invoice(int id);
    Task<IEnumerable<Invoice>> GetAllInvoices();
    Task<Customer> CreateCustomer(Human human);
    Task<Customer> DeleteCustomer_ByEmail(string Email);
    Task<Customer> GetCustomer_ById(int id);
    
    Task<Human> Update(int id);
    Task<IEnumerable<Customer>> GetAllCustomers();
}
