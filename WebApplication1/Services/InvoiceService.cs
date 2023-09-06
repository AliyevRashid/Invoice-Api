using Invoice_Api.Data;
using Invoice_Api.DTO;
using Invoice_Api.Models.Humans;
using Invoice_Api.Models;
using Invoice_Api.Services;
using Microsoft.EntityFrameworkCore;


namespace Invoice_Api.Services;

public class InvoiceService : IInvoiceService
{
    private InvoiceDbContext _context;

    public async Task<Invoice?> Create_Invoice(InvoiceRow_Request request)
    {
        var invoice_row = CreateInvoiceRow(request);
        var invoice_rowList = new List<InvoiceRow>();
        invoice_rowList.Add(invoice_row);
        var invoice = new Invoice() {Rows = invoice_rowList };
        invoice_row.InnvoiceId = invoice.Id;

         _context.InvoiceRow.Add(invoice_row);
        invoice =_context.Invoices.Add(invoice).Entity;

        await _context.SaveChangesAsync();
        return invoice;
    }
    public  Task<IEnumerable<Invoice>> GetAllInvoices()
    {
        var invoices =  _context.Invoices.ToList();
        return Task.FromResult(invoices.Select(item => item));
    }
    public async Task<Customer?> CreateCustomer(Human human)
    {
         if (human is Customer)
        {
            var customer = new Customer()
            { 
                Id = human.Id,
                UserName = human.UserName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            customer = _context.Customers.Add(customer).Entity;
            await _context.SaveChangesAsync();
            return customer;
        }
        return null;

    }

    public async Task<Customer> DeleteCustomer_ByEmail(string Email)
    {
        var deleted_customer = await _context.Customers.FindAsync(Email);
        if (deleted_customer != null)
        {
            _context.Customers.Remove(deleted_customer);
            _context.SaveChanges();
            return deleted_customer;
        }
        return null;
    }
   

    public Task<Invoice> Delete_Invoice(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Customer>> GetAllCustomers()
    {
        var customers = _context.Customers.ToList();
        return Task.FromResult(customers.Select(item => item));
    }

    public async Task<Customer> GetCustomer_ById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return customer is not null ?
                       customer : null;
    }
    public async Task<AppUser> GetUser_ById(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user is not null ?
                       user : null;
    }

    public async Task<Human> Update(int id)
    {
        throw new NotImplementedException();
    }

    public Task<Invoice> Update_Invoice(int id)
    {
        throw new NotImplementedException();
    }

    private AppUser CovnvertToUser(Human human)
    {
        var user = new AppUser() { Id = human.Id, UserName = human.UserName };
        return user;
    }

    private InvoiceRow CreateInvoiceRow(InvoiceRow_Request request)
    {
        var invoice_row = new InvoiceRow()
        { 
            Service = request.Service_name, 
            Quantity = request.Quantity, 
            Rate = request.Rate
        };
        return invoice_row;
    }
}
