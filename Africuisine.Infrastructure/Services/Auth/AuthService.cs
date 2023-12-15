using System.Net;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Config;
using Africuisine.Application.Exceptions;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.User;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Services.Postmark;
using Microsoft.AspNetCore.Identity;

namespace Africuisine.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserDM> UserManager;
        private readonly IPostmarkService Postmark;
        private readonly IUserService UserService;
        private PostmarkCommand Sender { get; set; }
        private readonly IJWTService JWTService;

        public AuthService(
            UserManager<UserDM> userManager,
            IJWTService jWTService,
            IPostmarkService postmark,
            IUserService userService
        )
        {
            UserManager = userManager;
            JWTService = jWTService;
            Postmark = postmark;
            UserService = userService;
        }

        public async Task<AuthResponse> SignInWithPasswordAndEmail(UserLoginCommand request)
        {
            var user = await UserManager.FindByEmailAsync(request.Username);
            if (user is not null)
            {
                if (await UserManager.CheckPasswordAsync(user, request.Password))
                {
                    var response = await UserService.GetAuthenticatedUserDetails(user.Email);
                    var claims = JWTService.GenerateClaims(response.Item);
                    string token = JWTService.GenerateJWTToken(claims);
                    return new AuthResponse
                    {
                        Token = token,
                        Succeeded = !string.IsNullOrEmpty(token)
                    };
                }
            }
            return new AuthResponse { Message = "Invalid user credentials", Succeeded = false };
        }

        public async Task<AuthResponse> ResetPasswordWithEmail(ForgotPasswordCommand command)
        {
            var user = await UserManager.FindByEmailAsync(command.Email);
            if (user is not null)
            {
                var token = await UserManager.GeneratePasswordResetTokenAsync(user);
                string URI = $"auth/reset-password?token={token}";
                command.Uri += command.Uri.EndsWith('/') ? $"{URI}" : $"/{URI}";
                var response = await Postmark.SendTemplateEmail(user, token, "reset-password");
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

        public async Task<AuthResponse> UpdatePassword(PasswordResetTokenCommand command)
        {
            var user = await UserManager.FindByEmailAsync(command.Email);
            if (user is not null)
            {
                var response = await UserManager.ResetPasswordAsync(
                    user,
                    command.Token,
                    command.Password
                );
                return new AuthResponse { Succeeded = response.Succeeded };
            }
            throw new NotFoundException(
                HttpStatusCode.NotFound,
                $"User with '{command.Email}' email address does not exists."
            );
        }
    }
}
