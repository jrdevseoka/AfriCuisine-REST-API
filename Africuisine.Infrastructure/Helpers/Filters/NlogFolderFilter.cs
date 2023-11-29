using Microsoft.AspNetCore.Mvc.Filters;

namespace Africuisine.Infrastructure.Helpers
{
    public class NlogFolderFilter : IAsyncActionFilter
    {

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            string rootDir = AppDomain.CurrentDomain.BaseDirectory;
            string Logs = Path.Combine(rootDir, nameof(Logs));
            string Archive = Path.Combine(Logs, nameof(Archive));

            Directory.CreateDirectory(Logs);
            Directory.CreateDirectory(Archive);
            await next();
        }
    }
}