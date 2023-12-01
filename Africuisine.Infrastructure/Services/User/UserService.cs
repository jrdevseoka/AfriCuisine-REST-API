using System.Security.Claims;
using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Config;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Domain.Models.Pictures;
using Africuisine.Infrastructure.Services.Postmark;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Africuisine.Infrastructure.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly UserManager<UserDM> Manager;
        private readonly RoleManager<RoleDM> RoleManager;
        private readonly IPostmarkService Postmark;
        private readonly IJWTService JWTService;
        private JWTBearer JWT { get; set; }

        public UserService(
            INLogger logger,
            IOptions<JWTBearer> options,
            IMapper mapper,
            UserManager<UserDM> manager,
            RoleManager<RoleDM> roleManager,
            IPostmarkService postmark,
            IJWTService jWTService)
            : base(logger, mapper)
        {
            Manager = manager;
            RoleManager = roleManager;
            JWT = options.Value;
            Postmark = postmark;
            JWTService = jWTService;
        }

        public async Task<PostResponse> Create(CreateUserCommand command)
        {
            var user = Mapper.Map<UserDM>(command);
            var response = await Manager.CreateAsync(user, command.Password);
            if (response.Succeeded)
            {
                //Add User to role
                var role = await RoleManager.FindByIdAsync(command.LRole);
                response = await Manager.AddToRoleAsync(user, role.Name);
                if (response.Succeeded)
                {
                    var claims = JWTService.GenerateClaims(user, role.Name);
                    response = await Manager.AddClaimsAsync(user, claims);
                    if (response.Succeeded)
                    {
                        string token = await Manager.GenerateEmailConfirmationTokenAsync(user);
                        try
                        {
                            command.HostUri += GenerateEmailConfirmationURI(token, user.Email);
                            // var postmarkRes = await Postmark.SendTemplateEmail(
                            //     user,
                            //     command.HostUri,
                            //     "confirmation"
                            // );
                            // if (postmarkRes.Succeeded)
                            // {
                            //     return new PostResponse
                            //     {
                            //         Message = "Your account was successfully created.",
                            //         Succeeded = true
                            //     };
                            // }
                            return new PostResponse { Succeeded = true, Message = "Your account was successfully created."};
                        }
                        catch (Exception ex)
                        {
                            Logger.Error($"An error occured while attempting to create an account. Error:{ex.Message}", ex);
                            await Manager.DeleteAsync(user);
                            return new PostResponse { Message = "Account failed to be created. Please try again", Succeeded = false };
                        }
                        

                    }
                }
            }
            string message = GenerateErrorMessage(response.Errors);
            return new PostResponse { Message = message };
        }

        public async Task<QueryItemResponse<ProfileSM>> GetAuthenticatedUserDetails(string email)
        {
            var user = await Manager.FindByEmailAsync(email);
            var profile = Mapper.Map<ProfileSM>(user);

            string roleName = (await Manager.GetRolesAsync(user)).First();
            var role = await RoleManager.FindByNameAsync(roleName);
            var dtoRole = Mapper.Map<RoleSM>(role);
            profile.Role = dtoRole;
            //!TODO - Retrieve Profile picture where status is activated

            return new QueryItemResponse<ProfileSM> { Succeeded = profile is not null, Item = profile };
        }

        private static string GenerateErrorMessage(IEnumerable<IdentityError> errors)
        {
            return string.Format($"{Environment.NewLine}", errors.Select(err => err.Description));
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
            return string.Format("users/confirm?token={0}&email={1}", encodedToken, encodeEmail);
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

        public Task<QueryItemResponse<ProfilePictureSM>> SetProfilePicture(CreatePictureCommand command)
        {
            throw new NotImplementedException();
        }

        Task<PostResponse> IUserService.SetProfilePicture(CreatePictureCommand command)
        {
            throw new NotImplementedException();
        }

        Task<QueryItemResponse<PictureDM>> IUserService.ConfirmAccount(string email, string token)
        {
            throw new NotImplementedException();
        }
    }
}