using Africuisine.API.Config;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Auth
{
    public class ResetPasswordController : APIBaseController<UserDM>
    {
        private readonly IPasswordService PasswordService;

        public ResetPasswordController(IErrorService<UserDM> error, IPasswordService passwordService)
            : base(error)
        {
            PasswordService = passwordService;
        }

        [HttpPost("send-email")]
        public async Task<IActionResult> Reset([FromBody] ForgotPasswordCommand command)
        {
            try
            {
                var response = await PasswordService.GetResetPasswordToken(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var response = Error.MapErrorToAuthResponse(exception);
                return BadRequest(response);
            }
        }

        [HttpPost("update")]
        [AllowAnonymous]
        public async Task<IActionResult> Update([FromBody] PasswordResetTokenCommand command)
        {
            try
            {
                var response = await PasswordService.ResetPassword(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var response = Error.MapErrorToAuthResponse(exception);
                return BadRequest(response);
            }
        }
    }
}