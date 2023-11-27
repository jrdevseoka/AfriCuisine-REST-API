namespace Africuisine.Application.Res
{
    public class QueryItemsResponse<TEntity> : BaseResponse where TEntity : class
    {
        IReadOnlyList<TEntity> Items { get; set; }
    }
}