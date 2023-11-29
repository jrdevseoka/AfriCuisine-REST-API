using Africuisine.Application.Config;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Africuisine.Infrastructure;

public static class SettingsConfig
{
    public static IServiceCollection RegisterOptionsConfigurations(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<PostmarkCommand>(configuration.GetSection("Postmark"))
                 .Configure<JWTBearer>(configuration.GetSection("JWT"));
        return services;
    }
}
