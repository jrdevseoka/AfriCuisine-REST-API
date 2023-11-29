using Africuisine.Application.Config;
using Africuisine.Application.Res;
using Africuisine.Domain.Models;

namespace Africuisine.Infrastructure.Services.Postmark
{
    public interface IPostmarkService
    {
        Task<BaseResponse> SendTemplateEmail(UserDM recipient,string url, string template = "");
    }
}