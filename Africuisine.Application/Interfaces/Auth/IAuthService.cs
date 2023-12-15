using Africuisine.Application.Commands.User;
using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<AuthResponse> SignInWithPasswordAndEmail(UserLoginCommand request);
        Task<AuthResponse> ResetPasswordWithEmail(ForgotPasswordCommand command);
        Task<AuthResponse> UpdatePassword(PasswordResetTokenCommand command);
    }
}
