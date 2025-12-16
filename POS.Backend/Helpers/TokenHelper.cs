using Microsoft.IdentityModel.Tokens;
using POS.Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace POS.Backend.Helpers
{
    public class TokenHelper(IConfiguration config) : ITokenHelper
    {

        private readonly IConfiguration _config = config;

        private readonly string secretKey = config.GetSection("Jwt").GetValue<string>("key")!;
        public string GenerateToken(User user)
        {
            var keyBytes = Encoding.ASCII.GetBytes(secretKey);
            var claims = new ClaimsIdentity();
            claims.AddClaim(new Claim(ClaimTypes.Name, user.Email));
            claims.AddClaim(new Claim(ClaimTypes.Role, user.Role.ToString()));
            claims.AddClaim(new Claim("Nombre", user.Name));
            claims.AddClaim(new Claim("UserId", user.Id.ToString()));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claims,
                Expires = DateTime.UtcNow.AddMonths(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenConfig = tokenHandler.CreateToken(tokenDescriptor);

            string tokencreado = tokenHandler.WriteToken(tokenConfig);

            return tokencreado;
        }
    }
}

