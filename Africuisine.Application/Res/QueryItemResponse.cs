namespace Africuisine.Application.Res
{
    public class QueryItemResponse<TEntity> : BaseResponse
        where TEntity : class
    {
        public TEntity Item { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
