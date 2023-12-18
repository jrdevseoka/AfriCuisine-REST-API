using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.User;
using Africuisine.Infrastructure.Services.Auth;
using Africuisine.Infrastructure.Services.Log;
using Africuisine.Infrastructure.Services.Postmark;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Asp.Versioning;
using Africuisine.Infrastructure.Services.Error;
using Africuisine.Infrastructure.Services.User;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Infrastructure.Seeding;
using Africuisine.Application.Interfaces.Picture;
using Africuisine.Infrastructure.Services.Picture;

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
            .AddScoped<IJWTService, JWTService>()
            .AddScoped<IPasswordService, PasswordService>()
            .AddScoped<IPictureService, PictureService>();
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