using Africuisine.Application.Requests.Picture;

namespace Africuisine.Application.Requests.User
{
    public class ProfilePictureSM : DTOModelBase
    {
        public bool Activated { get; set; }
        public string LPicture { get; set; }
        public PictureSM Picture {get; set;}
    }
}
