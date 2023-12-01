namespace Africuisine.Application.Requests.User
{
    public class ProfileSM : ServiceModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleSM Role { get; set; }
        public ProfilePictureSM ProfilePicture { get; set; }
    }
}
