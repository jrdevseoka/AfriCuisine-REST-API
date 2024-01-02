using System.ComponentModel.DataAnnotations.Schema;

namespace Africuisine.Domain.Models.Pictures
{
    [Table("PROFILEPICTURES")]
    public class ProfilePictureDM : DataModelBase
    {
        public bool Activated { get; set; }
        public string LPicture { get; set; }

        [ForeignKey(nameof(LPicture))]
        public PictureDM Picture { get; set; }
    }
}
