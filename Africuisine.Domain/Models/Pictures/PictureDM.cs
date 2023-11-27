using Africuisine.Domain.Models;

namespace Africuisine.Domain.Models.Pictures
{
    public class PictureDM : DataModelBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
    }
}
