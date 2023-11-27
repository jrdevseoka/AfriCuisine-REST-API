using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Res;

namespace Africuisine.Infrastructure.Services.Auth
{
    public class AuthService : IAuthService
    {
        public Task<AuthResponse> GetResetPasswordToken(string email)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> ResetPassword(PasswordResetTokenCommand request)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> SignInWithPasswordAndEmail(UserLoginCommand user)
        {
            throw new NotImplementedException();
        }
    }
}