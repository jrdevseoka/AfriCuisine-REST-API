using Africuisine.API.Config;
using Africuisine.Application.Commands.Picture;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.Picture;
using Africuisine.Application.Interfaces.User;
using Africuisine.Domain.Models.Pictures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Picture
{
    public class PicturesController : APIBaseController<PictureDM>
    {
        private readonly IUserService UserService;
        public PicturesController(IErrorService<PictureDM> error, IUserService user) : base(error)
        {
            UserService = user;
        }
        [HttpPost("profile")]
        public async Task<IActionResult> SetProfile(CreatePictureCommand command)
        {
            var response = await UserService.SetProfilePicture(command);
            if(response.Succeeded)
            {
                return Ok(response);
            }
            return BadRequest(response);
        }
    }
}