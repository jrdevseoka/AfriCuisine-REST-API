using Africuisine.Application.Res;

namespace Africuisine.Application.Interfaces.Ingredients;

public interface IIngrCategoryService { 
    Task<PostResponse> Create(CreateCategoryCommand command);
    Task<QueryItemResponse<IngredientCategoryDTO>> GetIngredientCategoryById(string id);
    Task<QueryItemsResponse<IngredientCategoryDTO>> GetIngredientCategories(int page,
        int size);
    
}
