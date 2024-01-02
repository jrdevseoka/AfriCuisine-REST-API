namespace Africuisine.Application;

public class DTOModelBase
{
    public string Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }
    public string LUserUpdate { get; set; }
    public int Updated { get; set; }
}
