using Africuisine.Application.Interfaces.Log;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Config
{
    public class APIBaseController : Controller {
        protected string GenerateUrl() => string.Format("{0}://{1}/", Request.Scheme, Request.Host);
    }
}