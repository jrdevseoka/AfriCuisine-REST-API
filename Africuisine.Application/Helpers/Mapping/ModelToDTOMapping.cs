using Africuisine.Application.Requests;
using Africuisine.Application.Requests.Picture;
using Africuisine.Application.Requests.User;
using Africuisine.Domain.Ingredients;
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
            MapPictureDmToPictureSm();
            MapPictureDMToProfilePictureSM();
            MapRoleDMToRoleSM();
            MapUserDmToProfileSm();
        }
        public void MapIngredientDTOToIngredientDm()
        {
            CreateMap<IngredientCategoryDTO, IngredientCategoryDM>()
            .IncludeBase<DTOModelBase, IngredientCategoryDM>()
            .ReverseMap();
        }

        public void MapPictureDMToProfilePictureSM()
        {
            CreateMap<ProfilePictureDM, ProfilePictureSM>()
                .ForMember(dst => dst.Picture, opts => opts.MapFrom(src => src.Picture))
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
            CreateMap<RoleDM, RoleSM>();
        }

        public void MapUserDmToProfileSm()
        {
            CreateMap<UserDM, ProfileSM>();
        }
        public void MapPictureDmToPictureSm()
        {
            CreateMap<PictureDM, PictureSM>()
            .IncludeBase<DataModelBase, ServiceModelBase>();
        }
    }
}
