using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Africuisine.Infrastructure;

public class AuthDBContext : IdentityDbContext<UserDM, RoleDM, string>
{

}
