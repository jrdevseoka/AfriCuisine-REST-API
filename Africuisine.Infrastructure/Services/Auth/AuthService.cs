using Africuisine.Application.Commands.User;
using Africuisine.Application.Config;
using Africuisine.Application.Interfaces.Auth;
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
        private PostmarkCommand Sender { get; set; }
        private readonly IJWTService JWTService;

        public AuthService(
            UserManager<UserDM> userManager,
            IJWTService jWTService,
            IPostmarkService postmark
        )
        {
            UserManager = userManager;
            JWTService = jWTService;
            Postmark = postmark;
        }

        public async Task<AuthResponse> SignInWithPasswordAndEmail(UserLoginCommand request)
        {
            var user = await UserManager.FindByEmailAsync(request.Username);
            if (user is not null)
            {
                if (await UserManager.CheckPasswordAsync(user, request.Password))
                {
                    var roleName = (await UserManager.GetRolesAsync(user)).First();
                    var claims = JWTService.GenerateClaims(user, roleName);
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
    }
}
