using Africuisine.Domain.Enum;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace Africuisine.Infrastructure.Seeding
{
    public class SeedService
    {
        private readonly ILogger Logger = LogManager.GetCurrentClassLogger();

        public SeedService(ILogger logger)
        {
            Logger = logger;
        }

        public async Task SeedRole(RoleManager<RoleDM> RoleManager)
        {
            var roles = Enum.GetNames(typeof(ERole));
            foreach(var name in roles)
            {
               if(await RoleManager.RoleExistsAsync(name))
                {
                    var role = new RoleDM { Name = name };
                    var result = await RoleManager.CreateAsync(role);
                    if (!result.Succeeded)
                    {
                        Logger.Error(string.Join($"{Environment.NewLine}", result.Errors.Select(r => r.Description)));
                    }
                }
            }
            
        }
    }
}
