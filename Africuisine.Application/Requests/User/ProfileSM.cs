namespace Africuisine.Application.Requests.User
{
    public class ProfileSM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public RoleSM Role { get; set; }
        public ProfilePictureSM ProfilePicture { get; set; }
    }
}
