using Africuisine.Application.Commands.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.User
{
    public interface IUserService
    {
        Task<BaseResponse> Create(CreateUserCommand command);
        Task<QueryItemResponse<ProfileSM>> GetAuthenticatedUserDetails(string email);
    }
}