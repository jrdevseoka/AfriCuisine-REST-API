
using System.Net;

namespace Africuisine.Application.Exceptions
{
    public class BadRequestException : ApplicationException {
        public HttpStatusCode Code { get; private set; }    
        public BadRequestException(HttpStatusCode code, string message) : base(message)
        {
            Code = code;
        }
    }
}
