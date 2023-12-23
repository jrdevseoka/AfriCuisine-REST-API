
namespace Africuisine.Application.Requests.User
{
    public class ProfileSM : DTOModelBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Picture { get; set; }
    }
}
