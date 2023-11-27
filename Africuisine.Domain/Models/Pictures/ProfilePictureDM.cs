using System.ComponentModel.DataAnnotations.Schema;

namespace Africuisine.Domain.Models.Pictures
{
    [Table("PROFILEPICTURE")]
    public class ProfilePictureDM : DataModelBase
    {
        public string LUser { get; set; }
        public string LPicture { get; set; }

    }
}
