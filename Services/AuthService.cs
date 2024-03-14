using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.IdentityModel.Tokens;

namespace Token_based_Authentication___Authorization.Services
{
    public class AuthService
    {
        private readonly string _secretKey;
        private readonly int _tokenExpiration;
        private readonly string _issuer;
        private readonly string _audience;

        public AuthService(string secretKey, int tokenExpiration, string issuer, string audience)
        {
            _secretKey = secretKey;
            _tokenExpiration = tokenExpiration;
            _issuer = issuer;
            _audience = audience;
        }

        public string GenerateJwtToken(string username)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new[] { new Claim(ClaimTypes.Name, username) },
                expires: DateTime.UtcNow.AddMinutes(_tokenExpiration),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
