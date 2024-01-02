using Africuisine.Domain.Models.Pictures;
using System.ComponentModel.DataAnnotations.Schema;

namespace Africuisine.Domain.Models.Recipes
{
    [Table("RECPICTURES")]
    public class RecipePictureDM : DataModelBase
    {
       public string LRecipe { get; set; }
   

       public string LPicture { get; set; }
        [ForeignKey(nameof(LPicture))]
        public PictureDM Picture { get; set; }
        [ForeignKey(nameof(LRecipe))]
       public RecipeDM Recipe { get; set; }
    }
}
