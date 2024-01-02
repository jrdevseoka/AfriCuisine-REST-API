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
            MapPictureDmToPictureSm();
            MapPictureDMToProfilePictureSM();
            MapRoleDMToRoleSM();
            MapUserDmToProfileSm();
            MapDTOModelBaseToDataModelBase();
        
        }
        public void MapIngredientDTOToIngredientDm()
        {
            CreateMap<IngredientCategoryDTO, IngredientCategoryDM>()
            .IncludeBase<DTOModelBase, DataModelBase>()
            .ReverseMap();
        }

        public void MapPictureDMToProfilePictureSM()
        {
            CreateMap<ProfilePictureDM, ProfilePictureSM>()
                .ForMember(dst => dst.Picture, opts => opts.MapFrom(src => src.Picture))
                .IncludeBase<DataModelBase, DTOModelBase>();
        }

        public void MapRoleDMToRoleSM()
        {
            CreateMap<RoleDM, RoleSM>();
        }

        public void MapUserDmToProfileSm()
        {
            CreateMap<UserDM, ProfileSM>();
        }
        public void MapDTOModelBaseToDataModelBase()
        {
            CreateMap<DTOModelBase, DataModelBase>()
            .ForMember(dst => dst.Link, opts => opts.MapFrom(src => src.Id))
            .ForMember(dst => dst.Creation, opts => opts.MapFrom(src => src.Created))
            .ForMember(dst => dst.LastUpdate, opts => opts.MapFrom(src => src.LastModified))
            .ForMember(dst => dst.LUserUpdate, opts => opts.MapFrom(src => src.LUserUpdate))
            .ForMember(dst => dst.SeqNo, opts => opts.MapFrom(src => src.Updated));

        }
        public void MapPictureDmToPictureSm()
        {
            CreateMap<PictureDM, PictureSM>()
            .IncludeBase<DataModelBase, DTOModelBase>()
            .ReverseMap();
        }
    }
}
