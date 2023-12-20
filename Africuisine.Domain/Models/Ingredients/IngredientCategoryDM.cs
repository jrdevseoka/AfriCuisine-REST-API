using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Africuisine.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Domain.Ingredients;

[Table("INGRCATEGORIES")]
[Index(nameof(Name), IsUnique = true)]
public class IngredientCategoryDM : DataModelBase
{
    public string Name { get; set; }
    public string Description { get; set; }

    [NotMapped]
    public virtual Collection<IngredientDM> Ingredients { get; set; }
}
