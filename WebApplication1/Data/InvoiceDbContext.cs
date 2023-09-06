using Invoice_Api.Models;
using Invoice_Api.Models.Humans;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Api.Data;

public class InvoiceDbContext :IdentityDbContext
{
    public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
        : base(options)
    {}
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceRow> InvoiceRow => Set<InvoiceRow>();
}
