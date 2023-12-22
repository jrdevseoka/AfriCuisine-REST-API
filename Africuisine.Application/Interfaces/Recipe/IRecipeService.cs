using Africuisine.Application.Res;
using Africuisine.Application.Commands;
using Africuisine.Application.DTO;
using Africuisine.Application.Commands.Recipe;
using Africuisine.Application.DTO.Recipe;

namespace Africuisine.Application.Interfaces.Recipe
{
    public interface IRecipeService
    {
        Task<PostResponse> Create(CreateRecipeCommand command);
        Task<QueryItemsResponse<RecipeDTO>> GetRecipes();
        Task<QueryItemsResponse<RecipeDTO>> GetRecipeById(string id);
        Task<QueryItemsResponse<RecipeDTO>> GetRecipeByCategory(string category);
        Task<QueryItemsResponse<RecipeDTO>> GetRecipeByPopularity(string rating);
        Task<PostResponse> Update(RecipeDTO recipe);
        Task<PostResponse> Delete(string id);
    }
}