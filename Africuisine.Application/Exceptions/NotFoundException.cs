using System.Net;


namespace Africuisine.Application.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode Code { get; private set; }
        public NotFoundException(HttpStatusCode code, string message) : base(message) {
       
            Code = code;
        }

    }
}
