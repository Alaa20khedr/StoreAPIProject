using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Store.Data.Entities.IdentityEntity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Store.Service.TokenServices
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration configuration;
        private readonly SymmetricSecurityKey key;
        public TokenServices(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Token:Key"]));
        }
        public string GenerateToken(AppUser appUser)
        {
            var claims = new List<Claim>()
           {
               new Claim(ClaimTypes.Email, appUser.Email),
               new Claim(ClaimTypes.GivenName,appUser.DisplayName)
           };
            var creds = new SigningCredentials(this.key, SecurityAlgorithms.HmacSha256);
             var tokenDiscreptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Issuer = this.configuration["Token:Issuer"],
                    IssuedAt = DateTime.UtcNow,
                    Expires = DateTime.Now.AddDays(1),
                    SigningCredentials = creds,
                };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDiscreptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
