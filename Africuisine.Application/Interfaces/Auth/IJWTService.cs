using System.Security.Claims;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IJWTService
    {
        public Task<string> GenerateJWTToken(IEnumerable<Claim> claims);
    }
}