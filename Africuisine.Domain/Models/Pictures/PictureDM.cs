using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Africuisine.Domain.Models.Recipes;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Domain.Models.Pictures
{
    [Index(nameof(Name), IsUnique = true)]
    [Table("PICTURES")]
    public class PictureDM : DataModelBase
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }

        [NotMapped]
        public Collection<ProfilePictureDM> ProfilePictures { get; set; }
        [NotMapped]
        public Collection<RecipePictureDM> RecipePictures { get; set; }
    }
}
