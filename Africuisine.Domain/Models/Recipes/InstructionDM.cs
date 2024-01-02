using System.ComponentModel.DataAnnotations.Schema;

namespace Africuisine.Domain.Models.Recipes
{
    [Table("RECINSTRUCTIONS")]
    public class InstructionDM : DataModelBase
    {
        public string Description { get; set; }
        public string LRecipe { get; set; }
        [ForeignKey(nameof(LRecipe))]
        public RecipeDM Recipe { get; set; }
    }
}