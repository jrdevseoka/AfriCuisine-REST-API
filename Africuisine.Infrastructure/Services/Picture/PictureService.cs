using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.Picture;
using Africuisine.Application.Res;
using Africuisine.Domain.Models.Pictures;
using Africuisine.Infrastructure.Context;
using AutoMapper;

namespace Africuisine.Infrastructure.Services.Picture
{
    public class PictureService : BaseService, IPictureService
    {
        private readonly PictureDBContext Data;
        public PictureService(INLogger logger, IMapper mapper, PictureDBContext data) : base(logger, mapper)
        {
            Data = data;
        }

        public async Task<QueryItemResponse<ProfilePictureDM>> AddToUser(PictureDM picture)
        {
            var pic = Mapper.Map<ProfilePictureDM>(picture);
            pic.Activated = true;
            await Data.ProfilePictures.AddAsync(pic);
            return new QueryItemResponse<ProfilePictureDM>{ Item = pic, Succeeded= !string.IsNullOrEmpty(pic.Link)};
        }

        public async Task<QueryItemResponse<PictureDM>> Create(CreatePictureCommand command)
        {
            var picture = Mapper.Map<PictureDM>(command);
            await Data.Pictures.AddAsync(picture);
            return new QueryItemResponse<PictureDM>{ Item = picture, Succeeded = !string.IsNullOrEmpty(picture.Link)};
        }
    }
}