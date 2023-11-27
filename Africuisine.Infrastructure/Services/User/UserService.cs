using Africuisine.Application.Commands.User;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.User;
using Africuisine.Application.Requests.User;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Infrastructure.Services;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

public class UserService : BaseService, IUserService
{
    private readonly UserManager<UserDM> Manager;
    public UserService(INLogger logger, IMapper mapper, UserManager<UserDM> manager) : base(logger, mapper)
    {
        Manager = manager;
    }

    public Task<BaseResponse> Create(CreateUserCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<QueryItemResponse<ProfileSM>> GetAuthenticatedUserDetails(string email)
    {
        throw new NotImplementedException();
    }
}