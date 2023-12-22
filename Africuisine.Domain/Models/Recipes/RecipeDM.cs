using System.ComponentModel.DataAnnotations.Schema;
namespace Africuisine.Domain.Models.Recipes
{
    [Table("RECIPES")]
    public class RecipeDM : DataModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Duration { get; set; }
        public string LCategory { get; set; }
        public string LDifficulty { get; set; }
        [ForeignKey(nameof(LCategory))]
        public RecipeCategoryDM Category { get; set; }
        [ForeignKey(nameof(LDifficulty))]
        public RecipeLevelDM Difficulty { get; set; }
        [NotMapped]
        public ICollection<InstructionDM> Instructions { get; set; }
        [NotMapped]
        public ICollection<RecipePictureDM> Pictures { get; set; }
    }
}