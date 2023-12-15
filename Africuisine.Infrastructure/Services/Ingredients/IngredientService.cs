using Africuisine.Application;
using Africuisine.Application.DTO;
using Africuisine.Application.Res;

namespace Africuisine.Infrastructure;

public class IngredientService : IIngredientService
{
    public Task<PostResponse> Create(CreateIngredientsCommand command)
    {
        throw new NotImplementedException();
    }

    public Task<QueryItemResponse<IngredientDTO>> GetIngredientById(string id)
    {
        throw new NotImplementedException();
    }

    public Task<QueryItemsResponse<IngredientDTO>> GetIngredients()
    {
        throw new NotImplementedException();
    }
}
