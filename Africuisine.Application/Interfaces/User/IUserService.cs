using Africuisine.Application.Commands.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<PostResponse> Create(CreateUserCommand command);
        Task<QueryItemResponse<ProfileSM>> GetAuthenticatedUserDetails(string email);
    }
}