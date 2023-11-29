using System.Security.Claims;
using Africuisine.Domain.Models;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IJWTService
    {
        IEnumerable<Claim> GenerateClaims(UserDM user, string roleName);
        string GenerateJWTToken(IEnumerable<Claim> claims);
    }
}