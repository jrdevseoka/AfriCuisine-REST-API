using Africuisine.Application.Interfaces.Error;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Config
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/{version:apiVersion}/[controller]")]
    public class APIBaseController<TEntity> : Controller
        where TEntity : class
    {
        protected IErrorService<TEntity> Error;

        public APIBaseController(IErrorService<TEntity> error)
        {
            Error = error;
        }

        protected string GenerateUrl() => string.Format("{0}://{1}/", Request.Scheme, Request.Host);
    }
}
