using Africuisine.Application.Requests;
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
            MapServiceModelBaseToDataModelBase();
            MapPictureDMToProfilePictureSM();
            MapRoleDMToRoleSM();
            MapUserDmToProfileSm();
        }

        public void MapPictureDMToProfilePictureSM()
        {
            CreateMap<ProfilePictureDM, ProfilePictureSM>()
                .IncludeBase<DataModelBase, ServiceModelBase>();
        }

        public void MapServiceModelBaseToDataModelBase()
        {
            CreateMap<DataModelBase, ServiceModelBase>()
                .ForMember(dst => dst.Id, opts => opts.MapFrom(src => src.Link))
                .ForMember(dst => dst.Created, opts => opts.MapFrom(src => src.Creation))
                .ForMember(dst => dst.Updated, opts => opts.MapFrom(src => src.SeqNo))
                .ForMember(dst => dst.LastModfied, opts => opts.MapFrom(src => src.LastUpdate));
        }

        public void MapRoleDMToRoleSM()
        {
            CreateMap<RoleDM, RoleSM>()
            .IncludeBase<DataModelBase, ServiceModelBase>();
        }

        public void MapUserDmToProfileSm()
        {
            CreateMap<UserDM, ProfileSM>()
            .IncludeBase<DataModelBase, ServiceModelBase>();
        }
    }
}
