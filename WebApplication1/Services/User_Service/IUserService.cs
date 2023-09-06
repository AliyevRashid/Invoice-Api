using Invoice_Api.DTO;
using Invoice_Api.Models.Humans;

namespace Invoice_Api.Services.User_Service
{
    public interface IUserService
    {
        Task<AppUser> CreateUser(ReqisterRequest request);
        Task<AppUser> UpdateUser(int id,ReqisterRequest request);
        Task<AppUser> UpdatePassword(int id,string password);
        Task<AppUser> DeleteUser(int id);
        Task<AppUser> GetUser(int id);
    }
}
