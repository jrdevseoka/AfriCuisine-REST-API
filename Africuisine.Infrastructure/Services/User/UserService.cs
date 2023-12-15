using System.Security.Claims;
using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Config;
using Africuisine.Application.Interfaces.Auth;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.Picture;
using Africuisine.Application.Interfaces.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Services.Postmark;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Africuisine.Infrastructure.Services.User
{
    public class UserService : BaseService, IUserService
    {
        private readonly IPictureService PictureService;
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
            IJWTService jWTService,
            IPictureService pictureService
        )
            : base(logger, mapper)
        {
            Manager = manager;
            RoleManager = roleManager;
            JWT = options.Value;
            Postmark = postmark;
            JWTService = jWTService;
            PictureService = pictureService;
        }

        public async Task<PostResponse> Create(CreateUserCommand command)
        {
            var user = Mapper.Map<UserDM>(command);
            var response = await Manager.CreateAsync(user, command.Password);
            if (response.Succeeded)
            {
                var role = await RoleManager.FindByIdAsync(command.LRole);
                response = await Manager.AddToRoleAsync(user, role.Name);
                if (response.Succeeded)
                {
                    string token = await Manager.GenerateEmailConfirmationTokenAsync(user);
                    try
                    {
                        command.HostUri += GenerateEmailConfirmationURI(token, user.Email);
                        return new PostResponse
                        {
                            Succeeded = true,
                            Message = "Your account was successfully created."
                        };
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(
                            $"An error occured while attempting to create an account. Error:{ex.Message}",
                            ex
                        );
                        await Manager.DeleteAsync(user);
                        return new PostResponse
                        {
                            Message = "Account failed to be created. Please try again",
                            Succeeded = false
                        };
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
            // var picture = await PictureService.GetActivatedProfilePic(user);
            // profile.PictureUri = picture.Picture.Url;
            profile.Role = (await Manager.GetRolesAsync(user)).First();
            return new QueryItemResponse<ProfileSM>
            {
                Succeeded = profile is not null,
                Item = profile
            };
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

        public async Task<PostResponse> SetProfilePicture(CreatePictureCommand command)
        {
            using (var transaction = await PictureService.StartTransaction())
            {
                try
                {
                    var picResponse = await PictureService.Create(command);
                    if (picResponse.Succeeded)
                    {
                        var ppResponse = await PictureService.AddToUser(picResponse.Item);
                        if (ppResponse.Succeeded)
                        {
                            var user = await Manager.FindByIdAsync(command.LUser);
                            var response = await GetAuthenticatedUserDetails(user.Email);
                            if (response.Succeeded)
                            {
                                await transaction.CommitAsync();
                                int rows = await PictureService.Save();
                                await transaction.DisposeAsync();
                                return new PostResponse
                                {
                                    Message = "You have successfully added a new picture.",
                                    Succeeded = rows > 0
                                };
                            }
                        }
                    }
                    throw new BadHttpRequestException(
                        "An unexpected error occured while uploading a profile picture"
                    );
                }
                catch (Exception e)
                {
                    Logger.Warn(e.Message);
                    await transaction.RollbackAsync();
                    return new PostResponse { Succeeded = false, Message = e.Message };
                }
            }
        }
    }
}
