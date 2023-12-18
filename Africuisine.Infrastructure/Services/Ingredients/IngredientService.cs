using Africuisine.Application;
using Africuisine.Application.DTO;
using Africuisine.Application.Res;
using Africuisine.Domain;
using Africuisine.Infrastructure.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure;

public class IngredientService : IIngredientService
{
    private readonly IngredientDBContext Data;
    private readonly IMapper Mapper;

    public IngredientService(IngredientDBContext data, IMapper mapper)
    {
        Data = data;
        Mapper = mapper;
    }

    public async Task<PostResponse> Create(CreateIngredientsCommand command)
    {
        var ingredient = Mapper.Map<IngredientDM>(command);
        Data.Ingredients.Add(ingredient);
        int rows = await Data.SaveChangesAsync();
        return new PostResponse { Succeeded = rows > 0 };
    }

    public async Task<PostResponse> Delete(IngredientDTO category)
    {
        var ingredient = Mapper.Map<IngredientDM>(category);
        Data.Ingredients.Remove(ingredient);
        int rows = await Data.SaveChangesAsync();
        return new PostResponse { Succeeded = rows > 0 };
    }

    public async Task<QueryItemResponse<IngredientDTO>> GetIngredientById(string id)
    {
        var ingredient = await Data.Ingredients
            .ProjectTo<IngredientDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        return new QueryItemResponse<IngredientDTO>
        {
            Item = ingredient,
            Succeeded = ingredient is not null
        };
    }

    public async Task<QueryItemsResponse<IngredientDTO>> GetIngredients(
        int pageNumber,
        int pageSize
    )
    {
        var ingredients = await Data.Ingredients
            .ProjectTo<IngredientDTO>(Mapper.ConfigurationProvider)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();
        return new QueryItemsResponse<IngredientDTO>
        {
            Items = ingredients,
            Succeeded = ingredients.Count > 0
        };
    }

    public async Task<PostResponse> Update(IngredientDTO ingredientDTO)
    {
        var ingredient = Mapper.Map<IngredientDM>(ingredientDTO);
        Data.Ingredients.Update(ingredient);
        int rows = await Data.SaveChangesAsync();
        return new PostResponse { Succeeded = rows > 0 };
    }
}
