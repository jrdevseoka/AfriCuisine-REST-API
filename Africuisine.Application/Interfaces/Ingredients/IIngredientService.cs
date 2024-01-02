using Africuisine.Application.DTO;
using Africuisine.Application.Res;

namespace Africuisine.Application;

public interface IIngredientService
{
    Task<PostResponse> Create(CreateIngredientsCommand command);
    Task<QueryItemsResponse<IngredientDTO>> GetIngredients(int pageNumber, int pageSize);
    Task<QueryItemResponse<IngredientDTO>> GetIngredientById(string id);
    Task<PostResponse> Delete(IngredientDTO category);
    Task<PostResponse> Update(IngredientDTO category);
}
