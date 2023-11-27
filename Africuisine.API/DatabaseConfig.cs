using Africuisine.Domain.Models;
using Africuisine.Infrastructure;
using Microsoft.AspNetCore.Identity;

namespace Africuisine.API
{
    public static class DatabaseConfig
    {
        public static IServiceCollection RegisterDBContext(this IServiceCollection services)
        {
            services
            .AddIdentity<UserDM, RoleDM>()
            .AddEntityFrameworkStores<AuthDBContext>()
            .AddDefaultTokenProviders();;

            return services;
        }
    }
}