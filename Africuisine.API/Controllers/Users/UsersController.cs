using Africuisine.API.Config;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.User;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Users
{
    public class UsersController : APIBaseController<UserDM>
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService, IErrorService<UserDM> error) : base(error)
        {
            UserService = userService;
            Error = error;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create(CreateUserCommand request)
        {
            try
            {
                request.HostUri = GenerateUrl();
                var response = await UserService.Create(request);
                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet("profile")]
        [AllowAnonymous]
        public async Task<IActionResult> Profile([FromQuery]string username)
        {
            try
            {
                var response = await UserService.GetAuthenticatedUserDetails(username);
                return Ok(response);
            }
            catch(Exception exception)
            {
                var response = Error.MapErrorToItemResponse(exception);
                return BadRequest(response);
            }
        }
    }
}