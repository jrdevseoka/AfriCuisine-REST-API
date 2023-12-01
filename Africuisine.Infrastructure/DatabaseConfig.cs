using Africuisine.Application.Config;
using Africuisine.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Africuisine.Infrastructure
{
    public static class DatabaseConfig
    {
        public static IServiceCollection RegisterDBContext(this IServiceCollection services, Database database)
        {
            services.AddDbContext<AuthDBContext>(opts => {
                opts.UseSqlServer(database.Connection);
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
            });
            services.AddDbContext<PictureDBContext>(opts => {
                opts.UseQueryTrackingBehavior(QueryTrackingBehavior.TrackAll);
                opts.UseSqlServer(database.Connection);
            });
            return services;
        }
    }
}