﻿using Invoice_Api.Auth;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Invoice_Api.Services.Jwt_service
{
    public class JwtService : IJwtService
    {
        private readonly JwtConfig _jwtConfig;

        public JwtService(JwtConfig jwtConfig)
        {
            _jwtConfig = jwtConfig;
        }

        public string GenerateSecurityToken(string Email,
            IEnumerable<string> roles,
            IEnumerable<Claim> userClaims)
        {
            var claims = new[]
            {
               new Claim(ClaimsIdentity.DefaultNameClaimType,Email),
               new Claim(ClaimsIdentity.DefaultRoleClaimType,string.Join(' ',roles))
            }.Concat(userClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtConfig.Secret));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            
                issuer: _jwtConfig.Issuer,
                audience: _jwtConfig.Audience,
                expires: DateTime.UtcNow.AddMinutes(_jwtConfig.Expiration),
                signingCredentials: signingCredentials,
                claims: claims
            );

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);

            return accessToken;
        }
    }
}
