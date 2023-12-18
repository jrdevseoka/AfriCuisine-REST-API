using System.Net;
using System.Web;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Exceptions;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Services.Postmark;
using Microsoft.AspNetCore.Identity;

namespace Africuisine.Infrastructure.Services.Auth
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<UserDM> UserManager;
        private readonly IPostmarkService Postmark;

        public PasswordService(UserManager<UserDM> userManager, IPostmarkService postmark)
        {
            UserManager = userManager;
            Postmark = postmark;
        }

        public async Task<AuthResponse> GetResetPasswordToken(ForgotPasswordCommand command)
        {
            var user = await UserManager.FindByEmailAsync(command.Email);
            if (user is not null)
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                string URI = $"/reset-password?token={ HttpUtility.UrlEncode(token)}";
                command.Uri += command.Uri.EndsWith('/') ? $"{URI}" : $"/{URI}";
                var response = await Postmark.SendTemplateEmail(user, command.Uri, "forgot-password");
                return new AuthResponse
                {
                    Succeeded = response.Succeeded,
                    Message = "A link to reset your password has been sent to your email address."
                };
            }
            throw new NotFoundException(
                HttpStatusCode.NotFound,
                $"User with '{command.Email}' email address does not exists."
            );
        }

        public async Task<AuthResponse> ResetPassword(PasswordResetTokenCommand request)
        {
            var response = new AuthResponse();
            var user = await UserManager.FindByEmailAsync(request.Email);
            if (user is not null)
            {
                var passwordRes = await UserManager.ResetPasswordAsync(
                    user,
                    request.Token,
                    request.Password
                );
                if (passwordRes.Succeeded)
                {
                    response.Succeeded = passwordRes.Succeeded;
                    response.Message =
                        "Password for an account with email was successfully updated";
                    return response;
                }
                response.Message = string.Join(
                    $"{Environment.NewLine}",
                    passwordRes.Errors.Select(e => e.Description)
                );
                response.Succeeded = response.Succeeded;
                return response;
            }
            return new AuthResponse
            {
                Message = $"Account with {request.Email} email does not exist.",
                Succeeded = false
            };
        }
    }
}
