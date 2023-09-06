using System.Security.Claims;

namespace Invoice_Api.Services.Jwt_service
{
    public interface IJwtService
    {
        string GenerateSecurityToken(string Email,IEnumerable<string> roles,IEnumerable<Claim> userClaims);
    }
}
