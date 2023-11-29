using System.Security.Claims;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Config;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class UserService : BaseService, IUserService
{
    private readonly UserManager<UserDM> Manager;
    private readonly RoleManager<RoleDM> RoleManager;
    private readonly IJWTService JwtService;
    private JWTBearer JWT { get; set; }

    public UserService(
        INLogger logger,
        IOptions<JWTBearer> options,
        IMapper mapper,
        UserManager<UserDM> manager,
        RoleManager<RoleDM> roleManager
,
        IJWTService jwtService)
        : base(logger, mapper)
    {
        Manager = manager;
        RoleManager = roleManager;
        JWT = options.Value;
        JwtService = jwtService;
    }

    public async Task<PostResponse> Create(CreateUserCommand command)
    {
        string message = string.Empty;

        var user = Mapper.Map<UserDM>(command);
        var response = await Manager.CreateAsync(user, command.Password);
        if (response.Succeeded)
        {
            //Add User to role
            var role = await RoleManager.FindByIdAsync(command.LRole);
            response = await Manager.AddToRoleAsync(user, role.Name);
            if (response.Succeeded)
            {
                var claims = GenerateClaims(user, role);
                response = await Manager.AddClaimsAsync(user, claims);
                if (response.Succeeded)
                {
                    string token = await JwtService.GenerateJWTToken(claims);
                    //! TODO - Going to implement Send Email Confirmation
                    command.HostUri += GenerateEmailConfirmationURI(token, user.Email);
                    return new PostResponse
                    {
                        Message = "Your account was successfully created.",
                        Succeeded = !string.IsNullOrEmpty(token)
                    };
                }
            }
        }
        message = GenerateErrorMessage(response.Errors);
        return new PostResponse { Message = message };
    }

    public async Task<QueryItemResponse<ProfileSM>> GetAuthenticatedUserDetails(string email)
    {
        var user = await Manager.FindByEmailAsync(email);
        var profile = Mapper.Map<ProfileSM>(user);

        dynamic role = (await Manager.GetRolesAsync(user)).First();
        role = Mapper.Map<RoleSM>(role);
        profile.Role = role;
        //!TODO - Retrieve Profile picture where status is activated

        return new QueryItemResponse<ProfileSM> { Succeeded = profile is not null, Item = profile };
    }

    private static string GenerateErrorMessage(IEnumerable<IdentityError> errors)
    {
        return string.Format($"{Environment.NewLine} {errors.Select(err => err.Description)}");
    }

    public IEnumerable<Claim> GenerateClaims(UserDM user, RoleDM role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Name, user.Name),
            new("role", role.Name),
            new("sub", user.Id),
            new("aud", JWT.ValidAudience),
            new("jti", Guid.NewGuid().ToString()),
        };

        return claims;
    }

    private static string GenerateEmailConfirmationURI(string token, string email)
    {
        string encodeEmail = Uri.EscapeDataString(email);
        string encodedToken = Uri.UnescapeDataString(token);
        return string.Format("/users?confirm?token={0}&email={1}", encodedToken, encodeEmail);
    }

    public async Task<PostResponse> ConfirmAccount(string email, string token)
    {
        string message = "Invalid Account Confirmation Details.";
        var user = await Manager.FindByEmailAsync(email);
        if (user != null)
        {
            var response = await Manager.ConfirmEmailAsync(user, token);
            message = response.Succeeded
                ? $"Account with {email} email address has been verified."
                : $"Account with {email} could not be verified. Please try again.";
            return new PostResponse { Succeeded = response.Succeeded, Message = message };
        }
        return new PostResponse { Succeeded = false, Message = message };
    }
}
