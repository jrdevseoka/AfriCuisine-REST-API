using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.Picture;
using Africuisine.Application.Res;
using Africuisine.Domain.Models.Pictures;
using Africuisine.Infrastructure.Context;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Africuisine.Domain.Models;
using AutoMapper.QueryableExtensions;
using Africuisine.Application.Requests.Picture;

namespace Africuisine.Infrastructure.Services.Picture
{
    public class PictureService : BaseService, IPictureService
    {
        private readonly PictureDBContext Data;

        public PictureService(INLogger logger, IMapper mapper, PictureDBContext data)
            : base(logger, mapper)
        {
            Data = data;
        }

        public async Task<QueryItemResponse<ProfilePictureDM>> AddToUser(PictureDM picture)
        {
            var pic = new ProfilePictureDM
            {
                Activated = true,
                LPicture = picture.Link,
                LUserUpdate = picture.LUserUpdate
            };
            await Data.ProfilePictures.AddAsync(pic);
            return new QueryItemResponse<ProfilePictureDM>
            {
                Item = pic,
                Succeeded = !string.IsNullOrEmpty(pic.Link)
            };
        }

        public async Task<QueryItemResponse<PictureDM>> Create(CreatePictureCommand command)
        {
            var picture = Mapper.Map<PictureDM>(command);
            await Data.Pictures.AddAsync(picture);
            return new QueryItemResponse<PictureDM>
            {
                Item = picture,
                Succeeded = !string.IsNullOrEmpty(picture.Link)
            };
        }

        public async Task<PictureSM> GetActivatedProfilePic(UserDM user)
        {
            try
            {
                var profilepic = Data.ProfilePictures
                    .Where(x => x.LUserUpdate == user.Id)
                    .FirstOrDefault(x => x.Activated == true);

                var picture = await Data.Pictures
                    .ProjectTo<PictureSM>(Mapper.ConfigurationProvider)
                    .FirstOrDefaultAsync(x => x.Id == profilepic.LPicture);
                return picture;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.Message);
                return new PictureSM { };
            }
        }

        public async Task<int> Save()
        {
            return await Data.SaveChangesAsync();
        }

        public async Task<IDbContextTransaction> StartTransaction()
        {
            IDbContextTransaction transaction = await Data.Database.BeginTransactionAsync();
            return transaction;
        }
    }
}
