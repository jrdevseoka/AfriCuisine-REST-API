using Africuisine.Application.Commands.User;
using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IPasswordService
    {
        Task<AuthResponse> GetResetPasswordToken(ForgotPasswordCommand command);
        Task<AuthResponse> ResetPassword(PasswordResetTokenCommand request);
    }
}