using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Res;

namespace Africuisine.Infrastructure.Services.Error
{
    public class ErrorService<TEntity> : IErrorService<TEntity>
        where TEntity : class
    {
        private readonly INLogger Logger;

        public ErrorService(INLogger logger)
        {
            Logger = logger;
        }

        public AuthResponse MapErrorToAuthResponse(Exception exception)
        {
            var error = MapError(exception, out string message);
            Logger.Error(message, exception);
            return new AuthResponse { Error = error, Message = message.Split('.').First() };
        }

        public BaseResponse MapErrorToBaseResponse(Exception exception)
        {
            var error = MapError(exception, out string message);
            Logger.Error(message, exception);
            return new PostResponse { Error = error, Message = message.Split('.').First() };
        }

        public QueryItemResponse<TEntity> MapErrorToItemResponse(Exception exception)
        {
            var error = MapError(exception, out string message);
            Logger.Error(message, exception);
            return new QueryItemResponse<TEntity> { Error = error, Message = message.Split('.').First() };
        }

        public QueryItemsResponse<TEntity> MapErrorToItemsResponse(Exception exception)
        {
            var error = MapError(exception, out string message);
            Logger.Error(message, exception);
            return new QueryItemsResponse<TEntity> { Error = error, Message = message.Split('.').First() };
        }

        private static ErrorResponse MapError(Exception exception, out string message)
        {
            string name = nameof(TEntity).Split('.').LastOrDefault().ToLower();
            message = GenerateErrorMessage(name, exception);
            ErrorResponse error = new() { Message = message };
            return error;
        }

        private static string GenerateErrorMessage(string name, Exception exception) =>
            $"An unexpected error occured in {name} service. Actual Error: {exception.Message}. {exception.InnerException}";

        public PostResponse MapErrorToPostResponse(Exception exception)
        {
             var error = MapError(exception, out string message);
            Logger.Error(message, exception);
            return new PostResponse { Error = error, Message = message.Split('.').First() };
        }
    }
}