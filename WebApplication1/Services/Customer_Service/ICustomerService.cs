using Invoice_Api.DTO;
using Invoice_Api.DTO.Pagination;
using Invoice_Api.Models.Humans;

namespace Invoice_Api.Services.User_Service
{
    public interface ICustomerService
    {
        Task<Customer> CreateCustomer(Human human);
        Task<Customer> DeleteCustomer_ByEmail(string Email);
        Task<Customer> GetCustomer_ById(int id);

        Task<Customer> UpdateCustomer(int id,string adress,string PhoneNumber);
        Task<PaginationList<Customer>> GetAllCustomers(int page, int size);
    }
}
