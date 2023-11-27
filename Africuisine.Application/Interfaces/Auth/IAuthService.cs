using Africuisine.Application.Commands.User;
using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> SignInWithPasswordAndEmail(UserLoginCommand user);
        Task<AuthResponse> GetResetPasswordToken(string email);
        Task<AuthResponse> ResetPassword(PasswordResetTokenCommand request);
    }
}
