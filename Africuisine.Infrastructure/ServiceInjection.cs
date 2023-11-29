using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.User;
using Africuisine.Infrastructure.Services.Auth;
using Africuisine.Infrastructure.Services.Log;
using Africuisine.Infrastructure.Services.Postmark;
using Microsoft.AspNetCore.Routing;
using Africuisine.Application.Interfaces;
using Africuisine.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Asp.Versioning;

namespace Africuisine.Infrastructure
{
    public static class ServiceInjection
    {
        public static IServiceCollection RegisterServiceInjection(this IServiceCollection services)
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
        public static IServiceCollection APIVersionInjection(this IServiceCollection services)
        {
            services.Configure<RouteOptions>(opts => {
                opts.LowercaseUrls = true;
            }).AddApiVersioning()
            .AddApiExplorer( opts => {
                opts.DefaultApiVersion = new ApiVersion(1.0);
                opts.SubstituteApiVersionInUrl = true; 
            });
            return services;
        }
    }
}