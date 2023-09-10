using Invoice_Api.Models.Humans;
using Invoice_Api.Services;
using Invoice_Api.Services.User_Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _service;

        private readonly UserManager<AppUser> _userManager;

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
        /// Update customer by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="adress"></param>
        /// <param name="PhoneNumber"></param>
        /// <returns>Updated Customer</returns>

        [HttpPatch("{id}/update")]
        public async Task<ActionResult<Customer>> Update_Customer(int id, [FromBody] string adress, [FromBody] string PhoneNumber)
        {
           var customer = await _service.UpdateCustomer(id, adress, PhoneNumber);
            return customer;

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
            return user.Customers.ToList();

        }
        /// <summary>
        /// Delete Customer by Id
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("{UserEmail}")]
        public async Task<ActionResult<Customer>> DeleteCustomer(string userEmail, [FromBody] string email)
        {
            if (email == null)
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
}
