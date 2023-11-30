using Africuisine.Application.Commands.User;
using Africuisine.Domain.Models;
using AutoMapper;

namespace Africuisine.Application.Helpers.Mapping
{
    public class CommandMappingProfile : Profile
    {
        public CommandMappingProfile()
        {
            MapCreateUserCommandToUserDm();
        }
        public void MapCreateUserCommandToUserDm()
        {
            CreateMap<CreateUserCommand, UserDM>()
            .ForMember(dst => dst.UserName, opts => opts.MapFrom(src => src.Email));
        }
    }
}