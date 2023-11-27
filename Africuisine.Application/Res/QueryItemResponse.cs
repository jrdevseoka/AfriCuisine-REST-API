namespace Africuisine.Application.Res
{
    public class QueryItemResponse<TEntity>
        where TEntity : class
    {
        TEntity Item { get; set; }
    }
}