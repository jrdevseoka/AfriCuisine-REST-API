using Africuisine.Application.Commands.User;
using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IPasswordService
    {
        Task<AuthResponse> GetResetPasswordToken(string email, string uri);
        Task<AuthResponse> ResetPassword(PasswordResetTokenCommand request);
    }
}