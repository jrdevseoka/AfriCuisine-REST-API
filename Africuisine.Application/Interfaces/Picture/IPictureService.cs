using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Domain.Models.Pictures;

namespace Africuisine.Application.Interfaces.Picture
{
    public interface IPictureService
    {
        Task<QueryItemResponse<PictureDM>> Create(CreatePictureCommand command);
        Task<QueryItemResponse<ProfilePictureDM>> AddToUser(PictureDM picture);   
    }
}