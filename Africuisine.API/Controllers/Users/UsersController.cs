using Africuisine.API.Config;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.User;
using Africuisine.Domain.Models;
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
        public async Task<IActionResult> Create(CreateUserCommand request)
        {
            try
            {
                var response = await UserService.Create(request);
                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Profile([FromQuery]string email)
        {
            try
            {
                var response = await UserService.GetAuthenticatedUserDetails(email);
                return Ok(response);
            }
            catch(Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}