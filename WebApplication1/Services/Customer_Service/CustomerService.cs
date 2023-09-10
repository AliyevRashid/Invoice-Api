using Invoice_Api.Data;
using Invoice_Api.DTO;
using Invoice_Api.DTO.Pagination;
using Invoice_Api.Models.Humans;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Invoice_Api.Services.User_Service;

public class CustomerService : ICustomerService
{
    private InvoiceDbContext _context;

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
    public async Task<Customer> UpdateCustomer(int id, string adress, string PhoneNumber)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer is null)
        {
            return null;
        }
        if (adress is not null)
        {
            customer.Adress = adress;
        }
        if (PhoneNumber is not null)
        {
            customer.PhoneNumber = PhoneNumber;
        }
        _context.Customers.Update(customer);
        _context.SaveChanges();
        return customer;
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




    public async Task<PaginationList<Customer>> GetAllCustomers(int page, int size)
    {
        IQueryable<Customer> customers_query = _context.Customers;

        var totalCount = await customers_query.CountAsync();
        var customers = await customers_query.Skip((page - 1) * size).Take(size).ToListAsync();

        return new PaginationList<Customer>(customers.Select(customer => customer), new PaginationMeta(page, size, totalCount));
    }

    public async Task<Customer> GetCustomer_ById(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        return customer is not null ?
                       customer : null;
    }
    //public async Task<AppUser> CreateUser(ReqisterRequest request)
    //{
    //    if(request is not null)
    //    { 
    //        var createdUser = _context.Users.Find(request.Email);
    //        if(createdUser != null)
    //        {
    //            return null;
    //        }
    //          createdUser=new AppUser() 
    //         {
    //             Email= request.Email,
    //             PasswordHash=request.Password,
    //             PhoneNumber=request.PhoneNumber,
    //             Adress=request.Adress,

    //             CreatedAt = DateTime.UtcNow,
    //             UpdatedAt= DateTime.UtcNow

    //         };
    //        createdUser= _context.Users.Add(createdUser).Entity;

    //        await _context.SaveChangesAsync();

    //       return createdUser;
    //    }
    //    return null;
    //}

    //public async  Task<AppUser> DeleteUser(int id)
    //{
    //   if(id > 0) 
    //    {
    //        var deleteUser = _context.Users.Find(id);
    //        if(deleteUser != null) 
    //        {
    //            _context.Users.Remove(deleteUser);
    //            await _context.SaveChangesAsync();
    //            return deleteUser;
    //        }
    //        return null;

    //    }
    //    return null;
    //}

    //public async Task<AppUser> UpdatePassword(int id, string password)
    //{
    //   if(id>0 ) 
    //    {
    //        var updateUser= _context.Users.Find(id);
    //        if(updateUser != null && password != null) 
    //        {
    //            updateUser.PasswordHash = password;
    //            updateUser.UpdatedAt = DateTime.UtcNow;

    //            _context.Users.Update(updateUser);
    //            await _context.SaveChangesAsync();
    //            return updateUser;

    //        }
    //        return null;
    //    }
    //    return null;
    //}


    //public async Task<AppUser> UpdateUser(int id, ReqisterRequest request)
    //{
    //    if (id > 0)
    //    {
    //        var updateUser = _context.Users.Find(id);
    //        if (updateUser != null && request != null)
    //        {
    //           if(request.Email!=null)
    //            {
    //                updateUser.Email = request.Email;
    //            }
    //           if(request.PhoneNumber!=null)
    //            {
    //                updateUser.PhoneNumber = request.PhoneNumber;
    //            }
    //           if(request.Adress!=null) 
    //            {
    //                updateUser.Adress = request.Adress;
    //            }
    //           if(request.Name!=null)
    //            {
    //                updateUser.UserName = request.Name;
    //            }
    //            //if (request.Customers != null)
    //            //{
    //            //    updateUser.Customers = request.Customers.ToArray();
    //            //}

    //           updateUser.UpdatedAt = DateTime.UtcNow;
    //            _context.Users.Update(updateUser);
    //            await _context.SaveChangesAsync();
    //            return updateUser;
    //        }
    //        return null;
    //    }
    //    return null;
    //}
    //public Task<AppUser> GetUser(int id) 
    //{
    //    if (id > 0) 
    //    {
    //        var user = _context.Users.Find(id);
    //        return Task.FromResult(user);
    //    }
    //    return null;
    //}


}
