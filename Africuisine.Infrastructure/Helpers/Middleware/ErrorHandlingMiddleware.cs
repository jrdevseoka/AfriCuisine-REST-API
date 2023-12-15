using Africuisine.Application.Exceptions;
using Africuisine.Application.Interfaces.Log;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Africuisine.Infrastructure.Helpers.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate Next;
        private readonly INLogger Logger;

        public ErrorHandlingMiddleware(INLogger logger, RequestDelegate next)
        {
            Logger = logger;
            Next = next;
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await Next(context);
            }
            catch(Exception ex) {
                Logger.Error($"An unexpected error ocurred. Error: {ex.Message} {Environment.NewLine} {ex.InnerException.Message}.", ex);
                await HandleExceptionAsync(context, ex);
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            string message = exception.Message;
            switch(exception)
            {
                case NotFoundException:
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case BadRequestException:
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException:
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                    break;
                default:
                    context.Response.StatusCode = (int)(HttpStatusCode.InternalServerError);
                    break;
            }
            await context.Response.WriteAsync(message);
        }
    }
}
