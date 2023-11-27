namespace Africuisine.Application.Res
{
    public class QueryItemsResponse<TEntity> : BaseResponse
        where TEntity : class
    {
        public IReadOnlyList<TEntity> Items { get; set; }
        public ErrorResponse Error { get; set; }
    }
}
