using System.ComponentModel.DataAnnotations.Schema;

namespace Africuisine.Domain.Models.Pictures
{
    [Table("PROFILEPICTURES")]
    public class ProfilePictureDM : DataModelBase
    {
        public string LUser { get; set; }
        public string LPicture { get; set; }

    }
}
