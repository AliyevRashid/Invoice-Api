using Invoice_Api.Data;
using Invoice_Api.DTO;
using Invoice_Api.Models.Humans;
using Invoice_Api.Models;
using Invoice_Api.Services;
using Microsoft.EntityFrameworkCore;
using Invoice_Api.DTO.Pagination;

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
        invoice.Status = InvoiceStatus.Created;

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
    public async Task<Invoice> Delete_Invoice(int id)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if (invoice == null || invoice.Status == InvoiceStatus.Sent)
        {
            return null;
        }
       
        var invoiceRows = _context.InvoiceRow.Where(x => x.InnvoiceId == id).ToList();
        foreach (var item in invoiceRows)
        {
            item.InnvoiceId = 0;
        }

        _context.Invoices.Remove(invoice);
        _context.SaveChanges();
        return invoice;
    }
   
   


    public async Task<Invoice> Update_Invoice(int id,string comment)
    {
        var invoice = await _context.Invoices.FindAsync(id);
        if(invoice is  null || invoice.Status == InvoiceStatus.Sent)
        {
           return null;
        }

        invoice.Comment= comment;
        invoice.UpdatetAt = DateTimeOffset.UtcNow;
        _context.Update(invoice);
        return invoice;

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
