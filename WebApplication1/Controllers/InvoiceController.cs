using Invoice_Api.DTO;
using Invoice_Api.Models;
using Invoice_Api.Services;
using Invoice_Api.Models.Humans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Invoice_Api.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class InvoiceController : ControllerBase
{
    private readonly IInvoiceService _service;

    private readonly UserManager<AppUser> _userManager;

    public InvoiceController(IInvoiceService service, UserManager<AppUser> userManager)
    {
        _service = service;
        _userManager = userManager;
    }


    /// <summary>
    /// Create new Invoice
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Created Invoice</returns>
    [HttpPost("Invoice")]
    public async Task<ActionResult<Invoice>> PostInvoice([FromBody]InvoiceRow_Request request)
    {
       var created_invoice= await _service.Create_Invoice(request);
       return created_invoice == null ? NotFound() : created_invoice;
    }
   /// <summary>
   /// Create new Customer
   /// </summary>
   /// <param name="customer"></param>
   /// <returns>Created Customer</returns>
    [HttpPost("Customer")]
    public async Task<ActionResult<Human>> Post_Customer([FromBody] Customer customer)
    {
        var created_customer = await _service.CreateCustomer(customer);
        return created_customer;
    }

   


    /// <summary>
    /// Return customer by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Customer>> GetCustomer_By_Id(int id)
    {
        var customer = await _service.GetCustomer_ById(id);
        return customer != null
                ? customer
                : NotFound();

    }
    /// <summary>
    /// return of all customers of the selected user
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    [HttpGet("{email}")]
    public async Task<ActionResult<IEnumerable<Customer>>> GetCustomers(string email)
    {
       var user = await _userManager.FindByEmailAsync(email);
       return  user.Customers.ToList();

    }
    /// <summary>
    /// Delete Customer by Id
    /// </summary>
    /// <param name="userEmail"></param>
    /// <param name="email"></param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("{UserEmanil}")]
    public async Task<ActionResult<Customer>> DeleteCustomer(string userEmail,[FromBody] string email)
    {
        if(email == null) 
        {
            return Conflict();
        }
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user.Customers == null) { return Conflict("Haven't any customer"); }
        var deletedCustomer = user.Customers.FirstOrDefault(x => x.Email == email);
        if (deletedCustomer == null)
        {
            return Conflict("Haven't customer with this Email");
        }
        
        _service.DeleteCustomer_ByEmail(deletedCustomer.Email);
        return deletedCustomer;
    }
}
