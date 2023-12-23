using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Requests.Picture;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;
using Africuisine.Domain.Models.Pictures;
using Microsoft.EntityFrameworkCore.Storage;

namespace Africuisine.Application.Interfaces.Picture
{
    public interface IPictureService
    {
        Task<QueryItemResponse<PictureDM>> Create(CreatePictureCommand command);
        Task<QueryItemResponse<ProfilePictureDM>> AddToUser(PictureDM picture);
        Task<PictureSM> GetActivatedProfilePic(UserDM user);
        Task<IDbContextTransaction> StartTransaction();
        Task<int> Save();
           
    }
}