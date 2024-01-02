using System.Security.Claims;
using Africuisine.Application.Requests.User;
using Africuisine.Domain.Models;
using AutoMapper;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IJWTService
    {
        IEnumerable<Claim> GenerateClaims(ProfileSM user);
        string GenerateJWTToken(IEnumerable<Claim> claims);
    }
}