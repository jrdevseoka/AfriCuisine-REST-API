using Microsoft.AspNetCore.Identity;

namespace Africuisine.Domain.Models
{
    public class UserDM : IdentityUser<string>
    {
        public string Name { get; set; }
    }
}