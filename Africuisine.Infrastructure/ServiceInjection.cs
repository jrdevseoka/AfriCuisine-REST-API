using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.User;
using Africuisine.Infrastructure.Services.Auth;
using Africuisine.Infrastructure.Services.Log;
using Africuisine.Infrastructure.Services.Postmark;
using Microsoft.Extensions.DependencyInjection;

namespace Africuisine.Infrastructure
{
    public static class ServiceInjection
    {
        public static IServiceCollection RegisterAPIInjection(this IServiceCollection services)
        {
            services
            .AddScoped<IPostmarkService, PostmarkService>()
            .AddSingleton<INLogger, NLogger>()
            .AddSingleton(typeof(IErrorService<>), typeof(ErrorService<>))
            .AddScoped<IUserService, UserService>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IJWTService, JWTService>();
            return services;
        }
    }
}