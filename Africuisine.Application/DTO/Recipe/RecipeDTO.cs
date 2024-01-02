using Africuisine.Application.Requests.Picture;
using Africuisine.Domain.Models.Recipes;

namespace Africuisine.Application.DTO.Recipe
{
    public class RecipeDTO : DTOModelBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public string LCategory { get; set; }
        public List<InstructionDM> Instructions { get; set; }
        public List<PictureSM> Pictures { get; set; }
    }
}
