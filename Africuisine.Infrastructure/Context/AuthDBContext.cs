using Africuisine.Domain.Models;
using Africuisine.Domain.Models.User;
using Africuisine.Infrastructure.Seeding;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure;

public class AuthDBContext : IdentityDbContext<UserDM, RoleDM, string, UserClaimDM,UserRoleDM, UserLoginDM, RoleClaimDM,UserTokenDM>
{
    public AuthDBContext(DbContextOptions<AuthDBContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Your customizations here
        builder.IdentityUserCustomization();
        builder.RoleIdentityCustomization();
    }
}

