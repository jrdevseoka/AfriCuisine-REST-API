namespace Africuisine.Domain.Models
{
    public class DataModelBase
    {
        public string Id { get; set; }
        public DateTime Creation { get; set; }
        public DateTime LastUpdate { get; set; }
        public int SeqNo { get; set; }
    }
}