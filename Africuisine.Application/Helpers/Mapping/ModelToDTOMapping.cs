using Africuisine.Application.Requests.User;
using Africuisine.Domain.Models;
using Africuisine.Domain.Models.Pictures;
using AutoMapper;

namespace Africuisine.Application.Helpers.Mapping
{
    public class ModelToDTOMapping : Profile
    {
        public ModelToDTOMapping()
        {
             MapPictureDMToProfilePictureSM();
        }
        public void MapPictureDMToProfilePictureSM()
        {
            CreateMap<PictureDM, ProfilePictureSM>()
            .ForMember(dst => dst.Path, opts => opts.MapFrom(src => src.Url))
            .ForMember(dst => dst.Type, opts => opts.MapFrom(src => src.Type));

        }
        public void MapRoleDMToRoleSM()
        {
            CreateMap<RoleDM, RoleSM>();
        }
    }
}