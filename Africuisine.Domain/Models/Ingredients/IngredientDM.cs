using System.ComponentModel.DataAnnotations.Schema;
using Africuisine.Domain.Ingredients;
using Africuisine.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Domain;

[Table("INGREDIENTS")]
[Index(nameof(Name), IsUnique = true)]
public class IngredientDM : DataModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string LCategory { get; set; }

    [ForeignKey(nameof(LCategory))]
    public virtual IngredientCategoryDM IngredientCategory { get; set; }
}
