namespace Africuisine.Application.Config
{
    public class JWTBearer
    {
        public string Key { get; set; }
        public string ValidAudience { get; set; }
        public string ValidIssuer { get; set; }
    }
}
