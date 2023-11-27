using Africuisine.Application.Interfaces.Error;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Config
{
    public class APIBaseController<TEntity> : Controller where TEntity : class {
        protected IErrorService<TEntity> Error;

        public APIBaseController(IErrorService<TEntity> error)
        {
            Error = error;
        }

        protected string GenerateUrl() => string.Format("{0}://{1}/", Request.Scheme, Request.Host);
    }
}