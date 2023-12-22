
using Africuisine.Application.Requests.Picture;

namespace Africuisine.Application.Commands.Recipe
{
    public class CreateRecipeCommand
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Duration { get; set; }
        public string LCategory { get; set; }
        public string LDifficulty { get; set; }
        public List<string> Instructions { get; set; }
        public List<PictureSM> Pictures { get; set; }
    }
}
