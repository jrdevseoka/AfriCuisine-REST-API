namespace Africuisine.Application.Commands.User
{
    public class CreateUserCommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string LRole { get; set; }
        public string Confirmed { get; set; }
        public string HostUri { get; set; }
    }
}
