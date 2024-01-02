using System.Text;
using Africuisine.Application.Config;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Helpers.Filters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace Africuisine.Infrastructure
{
    public static class AuthenticationConfig
    {
        public static IServiceCollection RegisterIdentity(this IServiceCollection services)
        {
            services
                .AddIdentity<UserDM, RoleDM>(opts =>
                {
                    opts.SignIn.RequireConfirmedEmail = false;
                    opts.SignIn.RequireConfirmedAccount = false;
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequiredLength = 8;
                    opts.Password.RequireLowercase = true;
                    opts.Password.RequireNonAlphanumeric = true;
                    opts.Password.RequireDigit = true;
                    opts.Password.RequireLowercase = true;
                    opts.Password.RequireUppercase = true;
                })
                .AddEntityFrameworkStores<AuthDBContext>()
                .AddDefaultTokenProviders();
            ;

            return services;
        }
        public static IServiceCollection RegisterSwaggerGeneration(this IServiceCollection services)
        {
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("1.0", new OpenApiInfo
                {
                    Version = "1.0",
                    Title = "AfriCuisine REST API",
                    License = new OpenApiLicense { Name = "MIT"}
                });                
                options.OperationFilter<SwaggerDefaultValueFilter>();
                options.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        Description =
                            "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = ""
                    }
                );
                options.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
                        }
                    }
                );
            });
            return services;
        }
        public static IServiceCollection RegisterAuthInjections(
            this IServiceCollection services,
            JWTBearer options
        )
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(opts =>
                {
                    opts.SaveToken = true;
                    opts.RequireHttpsMetadata = false; //Change to true during deployment
                    opts.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = options.ValidAudience,
                        ValidIssuer = options.ValidIssuer,
                        ClockSkew = TimeSpan.Zero,
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(options.Key)
                        )
                    };
                });
            return services;
        }
    }
    
}
