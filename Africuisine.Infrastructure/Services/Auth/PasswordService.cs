using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace Africuisine.Infrastructure.Services.Auth
{
    public class PasswordService : IPasswordService
    {
        private readonly UserManager<UserDM> UserManager;

        public PasswordService(UserManager<UserDM> userManager)
        {
            UserManager = userManager;
        }

        public async Task<AuthResponse> GetResetPasswordToken(string email, string uri)
        {
            var user = await UserManager.FindByEmailAsync(email);
            if (user is not null)
            {
                string token = await UserManager.GeneratePasswordResetTokenAsync(user);
                uri += $"reset-password?token={token}";
                return new AuthResponse
                {
                    Token = token,
                    Message = $"A password reset link was sent to {email}."
                };
            }
            return new AuthResponse
            {
                Message = $"Account with {email} email does not exist.",
                Succeeded = false
            };
        }

        public async Task<AuthResponse> ResetPassword(PasswordResetTokenCommand request)
        {
            var response = new AuthResponse();
            var user = await UserManager.FindByEmailAsync(request.Email);
            if(user is not null)
            {
                var passwordRes = await UserManager.ResetPasswordAsync(user, request.Token, request.Password);
                if(passwordRes.Succeeded)
                {
                    response.Succeeded = passwordRes.Succeeded;
                    response.Message = "Password for an account with email was successfully updated";
                    return response;
                }
                response.Message = string.Join($"{Environment.NewLine}", passwordRes.Errors.Select(e => e.Description));
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