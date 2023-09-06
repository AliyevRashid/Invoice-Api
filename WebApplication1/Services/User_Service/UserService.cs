using Invoice_Api.Data;
using Invoice_Api.DTO;
using Invoice_Api.Models.Humans;
using Microsoft.AspNetCore.Mvc;

namespace Invoice_Api.Services.User_Service;

public class UserService : IUserService
{
    private InvoiceDbContext _context;
    public async Task<AppUser> CreateUser(ReqisterRequest request)
    {
        if(request is not null)
        { 
            var createdUser = _context.Users.Find(request.Email);
            if(createdUser != null)
            {
                return null;
            }
              createdUser=new AppUser() 
             {
                 Email= request.Email,
                 PasswordHash=request.Password,
                 PhoneNumber=request.PhoneNumber,
                 Adress=request.Adress,

                 CreatedAt = DateTime.UtcNow,
                 UpdatedAt= DateTime.UtcNow
                 
             };
            createdUser= _context.Users.Add(createdUser).Entity;
            
            await _context.SaveChangesAsync();

           return createdUser;
        }
        return null;
    }

    public async  Task<AppUser> DeleteUser(int id)
    {
       if(id > 0) 
        {
            var deleteUser = _context.Users.Find(id);
            if(deleteUser != null) 
            {
                _context.Users.Remove(deleteUser);
                await _context.SaveChangesAsync();
                return deleteUser;
            }
            return null;

        }
        return null;
    }

    public async Task<AppUser> UpdatePassword(int id, string password)
    {
       if(id>0 ) 
        {
            var updateUser= _context.Users.Find(id);
            if(updateUser != null && password != null) 
            {
                updateUser.PasswordHash = password;
                updateUser.UpdatedAt = DateTime.UtcNow;

                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
                return updateUser;

            }
            return null;
        }
        return null;
    }

    
    public async Task<AppUser> UpdateUser(int id, ReqisterRequest request)
    {
        if (id > 0)
        {
            var updateUser = _context.Users.Find(id);
            if (updateUser != null && request != null)
            {
               if(request.Email!=null)
                {
                    updateUser.Email = request.Email;
                }
               if(request.PhoneNumber!=null)
                {
                    updateUser.PhoneNumber = request.PhoneNumber;
                }
               if(request.Adress!=null) 
                {
                    updateUser.Adress = request.Adress;
                }
               if(request.Name!=null)
                {
                    updateUser.UserName = request.Name;
                }
                //if (request.Customers != null)
                //{
                //    updateUser.Customers = request.Customers.ToArray();
                //}

               updateUser.UpdatedAt = DateTime.UtcNow;
                _context.Users.Update(updateUser);
                await _context.SaveChangesAsync();
                return updateUser;
            }
            return null;
        }
        return null;
    }
    public Task<AppUser> GetUser(int id) 
    {
        if (id > 0) 
        {
            var user = _context.Users.Find(id);
            return Task.FromResult(user);
        }
        return null;
    }


}
