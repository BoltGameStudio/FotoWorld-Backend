using FotoWorldBackend.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FotoWorldBackend.Services.Token
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        public TokenService(IConfiguration config)
        {
            _config = config;   
        }

        public string GenerateToken(User user, bool isOperator)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("SECRET_KEY").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("username", user.Username),
                new Claim("email", user.Email),
                new Claim("isOperator", Convert.ToString(isOperator))

            };
           
            string issuer = _config.GetSection("Urls:BackendUrl").Value;
            string audience = _config.GetSection("Urls:FrontendUrl").Value;

            var token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
