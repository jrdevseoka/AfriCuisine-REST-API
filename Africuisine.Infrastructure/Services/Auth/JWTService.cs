using System.Security.Claims;
using Africuisine.Application.Config;
using Africuisine.Application.Interfaces.Auth;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Africuisine.Domain.Models;
using Africuisine.Application.Requests.User;

namespace Africuisine.Infrastructure.Services.Auth
{
    public class JWTService : IJWTService
    {
        private JWTBearer JWT { get; set; }

        public JWTService(IOptions<JWTBearer> options)
        {
            JWT = options.Value;
        }

        public string GenerateJWTToken(IEnumerable<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.Key));

            var descriptor = new SecurityTokenDescriptor
            {
                Issuer = JWT.ValidIssuer,
                Audience = JWT.ValidAudience,
                Expires = DateTime.UtcNow.AddHours(5),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                Subject = new ClaimsIdentity(claims),
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateJwtSecurityToken(descriptor);
            return tokenHandler.WriteToken(token);
        }

        public IEnumerable<Claim> GenerateClaims(ProfileSM user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Email, user.Email),
                new(ClaimTypes.Name, user.Name),
                new(ClaimTypes.Role, user.Role),
                new("exp", DateTime.UtcNow.AddHours(5).ToLongDateString()),
                new("aud", JWT.ValidAudience),
                new("jti", Guid.NewGuid().ToString()),
            };

            return claims;
        }
    }
}
