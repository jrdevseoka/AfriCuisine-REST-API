using Africuisine.API.Config;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Users
{
    public class RolesController : APIBaseController<RoleDM>
    {
        private readonly RoleManager<RoleDM> RoleManager;
        public RolesController(IErrorService<RoleDM> error, RoleManager<RoleDM> roleManager) : base(error)
        {
            RoleManager = roleManager;
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Add([FromBody]string name) {
           try
            {
                var role = new RoleDM { Name = name, Id = Guid.NewGuid().ToString() };
                var response = await RoleManager.CreateAsync(role);
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = Error.MapErrorToBaseResponse(ex);
                return BadRequest(response);
            }
        }
    }
}
