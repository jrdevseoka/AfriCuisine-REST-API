using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Commands.User;
using Africuisine.Application.DTO;
using Africuisine.Domain;
using Africuisine.Domain.Ingredients;
using Africuisine.Domain.Models;
using Africuisine.Domain.Models.Pictures;
using AutoMapper;

namespace Africuisine.Application.Helpers.Mapping
{
    public class CommandMappingProfile : Profile
    {
        public CommandMappingProfile()
        {
            MapDataModelBaseToDTOModelBase();
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
        public void MapDataModelBaseToDTOModelBase(){
            CreateMap<DataModelBase, DTOModelBase>()
            .ForMember(dst => dst.Id, opts => opts.MapFrom(src => src.Link))
            .ForMember(dst => dst.LastModified, opts => opts.MapFrom(src => src.LastUpdate))
            .ForMember(dst => dst.Created, opts => opts.MapFrom(src => src.Creation))
            .ForMember(dst => dst.LUserUpdate, opts => opts.MapFrom(src => src.LUserUpdate))
            .ForMember(dst => dst.Updated, opts => opts.MapFrom(src => src.SeqNo));
        }
        public void MapIngredientCategoriesDMToDTO() {
            CreateMap<IngredientCategoryDM, IngredientCategoryDTO>()
            .IncludeBase<DataModelBase, DTOModelBase>();
        }
        public void MapIngredientDMToDTO() {
            CreateMap<IngredientDM, IngredientDTO>()
            .IncludeBase<DataModelBase, DTOModelBase>();
        }
    }
}