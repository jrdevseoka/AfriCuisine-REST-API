namespace Africuisine.Application.Commands.User
{
    public class UserLoginCommand
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Remembered { get; set; } = false;
    }
}
