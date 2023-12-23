using Africuisine.API.Config;
using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.User;
using Africuisine.Application.Requests.Picture;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Users
{
    public class UsersController : APIBaseController<UserDM>
    {
        private readonly IUserService UserService;

        public UsersController(IUserService userService, IErrorService<UserDM> error)
            : base(error)
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
                request.Uri = GenerateUrl();
                var response = await UserService.Create(request);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var response = Error.MapErrorToPostResponse(exception);
                return BadRequest(response);
            }
        }

        [HttpGet("confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> Confirm([FromQuery] string token, [FromQuery] string email)
        {
            try
            {
                var response =  await UserService.ConfirmAccount(email, token);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var response = Error.MapErrorToPostResponse(exception);
                return BadRequest(response);
            }
        }
    }
}
