using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Commands.User;
using Africuisine.Domain.Models;
using Africuisine.Domain.Models.Pictures;
using AutoMapper;

namespace Africuisine.Application.Helpers.Mapping
{
    public class CommandMappingProfile : Profile
    {
        public CommandMappingProfile()
        {
            MapCreateUserCommandToUserDm();
            MapCreaterictureCommandToUserDm();
        }
        public void MapCreateUserCommandToUserDm()
        {
            CreateMap<CreateUserCommand, UserDM>()
            .ForMember(dst => dst.UserName, opts => opts.MapFrom(src => src.Email));
        }
        public void MapCreaterictureCommandToUserDm()
        {
            CreateMap<CreatePictureCommand, PictureDM>()
            .ForMember(dst => dst.Url, opts => opts.MapFrom(src => src.Path))
            .ForMember(dst => dst.LUserUpdate, opts => opts.MapFrom(src => src.LUser));
        }
        public void MapPictureDmToProfilePicDm()
        {
            CreateMap<PictureDM, ProfilePictureDM>()
            .ForMember(dst => dst.LUserUpdate, opts => opts.MapFrom(src => src.LUserUpdate));
        }
    }
}