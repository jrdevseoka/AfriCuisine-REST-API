using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Africuisine.Domain.Models.Recipes
{
    [Table("RECDIFFICULTY")]
    public class RecipeLevelDM : DataModelBase
    {
        public string Name { get; set; }
    }
}
