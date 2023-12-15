using Africuisine.Application.DTO;
using Africuisine.Application.Res;

namespace Africuisine.Application;

public interface IIngredientService
{
    Task<PostResponse> Create(CreateIngredientsCommand command);
    Task<QueryItemsResponse<IngredientDTO>> GetIngredients();
    Task<QueryItemResponse<IngredientDTO>> GetIngredientById(string id);
}
