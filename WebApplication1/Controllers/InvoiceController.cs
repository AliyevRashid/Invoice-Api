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

    [HttpPost("DeleteInvocie_ById")]

    public async Task<ActionResult<Invoice>> DeleteInvoice([FromBody] int invoiceId )
    {
        var deletedInvoice = await _service.Delete_Invoice(invoiceId);
        return deletedInvoice== null ? NotFound() : deletedInvoice;
    }

  
}
