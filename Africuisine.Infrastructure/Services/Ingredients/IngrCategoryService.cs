using Africuisine.Application;
using Africuisine.Application.Interfaces.Ingredients;
using Africuisine.Application.Res;

namespace Africuisine.Infrastructure;

public class IngrCategoryService : IIngrCategoryService
{
    public Task<PostResponse> Create(CreateCategoryCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<QueryItemsResponse<IngredientCategoryDTO>> GetIngredientCategories()
    {
        throw new NotImplementedException();
    }

    public Task<QueryItemResponse<IngredientCategoryDTO>> GetIngredientCategoryById(string id)
    {
        throw new NotImplementedException();
    }
}
