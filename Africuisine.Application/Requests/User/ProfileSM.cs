using System.Security.Cryptography.X509Certificates;

namespace Africuisine.Application.Requests.User
{
    public class ProfileSM : ServiceModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string PictureUri { get; set; }
    }
}
