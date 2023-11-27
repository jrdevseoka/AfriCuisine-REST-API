namespace Africuisine.Application.Res
{
    public class AuthResponse : BaseResponse
    {
        public string Token { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
