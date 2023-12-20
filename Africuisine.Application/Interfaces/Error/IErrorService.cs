using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.Error
{
    public interface IErrorService<TEntity> where TEntity : class
    {
        AuthResponse MapErrorToAuthResponse(Exception exception);
        BaseResponse MapErrorToBaseResponse(Exception exception);
        PostResponse MapErrorToPostResponse(Exception exception);
        QueryItemResponse<TEntity> MapErrorToItemResponse(Exception exception);
        QueryItemsResponse<TEntity> MapErrorToItemsResponse(Exception exception);
    }
}