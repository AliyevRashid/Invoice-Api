using Invoice_Api.Models.Humans;
using Microsoft.AspNetCore.Mvc;
using Invoice_Api.Services;
using Microsoft.EntityFrameworkCore;
using Invoice_Api.Data;
using Microsoft.AspNetCore.Identity;
using Invoice_Api.Services.User_Service;
using Invoice_Api.DTO;
using Microsoft.AspNetCore.Authorization;
using Invoice_Api.Services.Jwt_service;

namespace Invoice_Api.Controllers
{
    public class AuthController : Controller
    {
       

        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtService _jwtService;

        public AuthController(UserManager<AppUser> userManager
            , SignInManager<AppUser> signInManager,
            IJwtService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }
        /// <summary>
        /// Create and registration user
        /// </summary>
        /// <param name="Register"></param>
        /// <returns>AuthTokem(AccessToken and RefreshToken)</returns>
        [HttpPost("Register")]
        public async Task<ActionResult<AuthTokenDto>> Register([FromBody] ReqisterRequest request)
        {
            var created_user = await _userManager.FindByEmailAsync(request.Email);
            if (created_user is not null) 
            {
                return Conflict("user alredy exists");
            }

            created_user = new AppUser()
            {
                UserName = request.Name,
                Email = request.Email,
                PasswordHash = request.Password,
                PhoneNumber = request.PhoneNumber,
                Adress = request.Adress,
               
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                RefreshToken = Guid.NewGuid().ToString("N").ToLower()
            };
            var result = await _userManager.CreateAsync(created_user, request.Password);
            if(!result.Succeeded)
            {
                return BadRequest();
            }
            return await GenerateToken(created_user);
        }
        /// <summary>
        /// Login
        /// </summary>
        /// <param name="request"></param>
        /// <returns>AuthTokem(AccessToken and RefreshToken)</returns>
        [HttpPost("Login")]
        public async Task<ActionResult<AuthTokenDto>> Login([FromBody] LoginRequest request)
        {
            var created_user = await _userManager.FindByEmailAsync(request.Email);
            if (created_user is  null)
            {
                return Conflict("AppUser not found");
            }
            var canSignIn = await _signInManager.CheckPasswordSignInAsync(created_user, request.Password, false);
            
            if (!canSignIn.Succeeded)
            {
                return Unauthorized();
            }
            var role = await _userManager.GetRolesAsync(created_user);
            var claims = await _userManager.GetClaimsAsync(created_user);

            var accessToken = _jwtService.GenerateSecurityToken(created_user.UserName, role, claims);
             var refreshToken = Guid.NewGuid().ToString("N").ToLower();
            created_user.RefreshToken = refreshToken;

            await _userManager.UpdateAsync(created_user);

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
            };
        }
        /// <summary>
        /// Only Admin Can Update User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="user"></param>
        /// <returns>Updated User</returns>
        [HttpPost("updateUser")]
        public async Task<ActionResult<Human>> Update_User(
            [FromBody] int id,
            [FromBody] string Name,
            [FromBody] string PhoneNumber)
        {
            var updated_user = await _userManager.FindByIdAsync(id.ToString());
            if (updated_user is  null)
            {
                return BadRequest();
            }
            updated_user.UserName= Name;
            updated_user.PhoneNumber = PhoneNumber; 
            updated_user.UpdatedAt= DateTime.UtcNow;
            await _userManager.UpdateAsync(updated_user);
            return updated_user;
        }



        [Authorize (Roles = "admin")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppUser>> GetUser_By_Id(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            return user != null
                    ? user
                    : NotFound();
        }

        [Authorize (Roles = "admin")]
        [HttpDelete]
        public async Task<AppUser> DeleteUser_ById(int id)
        {
            var deleted_User = await _userManager.FindByIdAsync(id.ToString());
            if (deleted_User != null)
            {
                await _userManager.DeleteAsync(deleted_User);

                
                return deleted_User;
            }
            return null;
        }



        private async Task<AuthTokenDto> GenerateToken(AppUser user)
        {
            var role = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);

            var accessToken = _jwtService.GenerateSecurityToken(user.UserName, role, userClaims);
            var refreshToken = user.RefreshToken;

            await _userManager.UpdateAsync(user);

            return new AuthTokenDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}
