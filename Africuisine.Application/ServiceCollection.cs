using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Africuisine.Application;

public static class ServiceCollection
{
    public static IServiceCollection RegisterApplicationInjections(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        return services;
    }
}
