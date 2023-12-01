using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Commands.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;
using Africuisine.Domain.Models.Pictures;

namespace Africuisine.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<PostResponse> Create(CreateUserCommand command);
        Task<PostResponse> SetProfilePicture(CreatePictureCommand command);
        Task<QueryItemResponse<ProfileSM>> GetAuthenticatedUserDetails(string email);
        Task<QueryItemResponse<PictureDM>> ConfirmAccount(string email, string token);
    }
}