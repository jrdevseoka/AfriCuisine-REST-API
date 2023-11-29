using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure;

public class AuthDBContext : IdentityDbContext<UserDM, RoleDM, string>
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options)
        : base(options) { }
}
