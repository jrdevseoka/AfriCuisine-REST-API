using System.Text;
using Africuisine.Application.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Africuisine.Infrastructure
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            
            return services;
        }
        public static IServiceCollection RegisterAuthInjections(this IServiceCollection services, JWTBearer options)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer( opts => {
                opts.SaveToken = true;
                opts.RequireHttpsMetadata = false; //Change to true during deployment
                opts.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = options.ValidAudience,
                    ValidIssuer = options.ValidIssuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.Key))
                };
            });
            return services;
        }
    }
}