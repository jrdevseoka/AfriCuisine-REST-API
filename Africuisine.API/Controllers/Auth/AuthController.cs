using Africuisine.API.Config;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Auth
{
    public class AuthController : APIBaseController<UserDM>
    {
        public AuthController(IErrorService<UserDM> error)
            : base(error) { }

        private readonly IAuthService Service;

        public AuthController(IAuthService service, IErrorService<UserDM> error)
            : base(error)
        {
            Service = service;
            Error = error;
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginCommand request)
        {
            try
            {
                var response = await Service.SignInWithPasswordAndEmail(request);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToAuthResponse(exception);
                return BadRequest(error);
            }
        }

        [HttpGet("request-password")]
        public async Task<IActionResult> RequestPasswordToken([FromQuery]string email)
        {
            try
            {
                var response = await Service.GetResetPasswordToken(email);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToAuthResponse(exception);
                return BadRequest(error);
            }
        }
        [HttpPost("update-password")]
        public async Task<IActionResult> Update(PasswordResetTokenCommand command)
        {
            try
            {
                var response = await Service.ResetPassword(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToAuthResponse(exception);
                return BadRequest(error);
            }
        }
    }
}
