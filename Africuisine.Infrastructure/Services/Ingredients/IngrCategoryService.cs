using Africuisine.Application;
using Africuisine.Application.Interfaces.Ingredients;
using Africuisine.Application.Res;
using Africuisine.Domain.Ingredients;
using Africuisine.Infrastructure.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace Africuisine.Infrastructure;

public class IngrCategoryService : IIngrCategoryService
{
    private readonly IMapper Mapper;
    private readonly IngredientDBContext Data;

    public IngrCategoryService(IMapper mapper, IngredientDBContext data)
    {
        Mapper = mapper;
        Data = data;
    }

    public async Task<PostResponse> Create(CreateCategoryCommand command)
    {
        var category = Mapper.Map<IngredientCategoryDM>(command);
        Data.IngredientCategories.Add(category);
        int rows = await Data.SaveChangesAsync();
        return new PostResponse { Succeeded = rows > 0 };
    }

    public async Task<QueryItemsResponse<IngredientCategoryDTO>> GetIngredientCategories(
        int page,
        int size
    )
    {
        var categories = await Data.IngredientCategories
            .ProjectTo<IngredientCategoryDTO>(Mapper.ConfigurationProvider)
            .Skip((page - 1) * size)
            .Take(size)
            .ToListAsync();
        return new QueryItemsResponse<IngredientCategoryDTO>
        {
            Items = categories,
            Succeeded = categories.Count > 0
        };
    }

    public async Task<QueryItemResponse<IngredientCategoryDTO>> GetIngredientCategoryById(string id)
    {
        var category = await Data.IngredientCategories
            .ProjectTo<IngredientCategoryDTO>(Mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(x => x.Id == id);
        return new QueryItemResponse<IngredientCategoryDTO>
        {
            Item = category,
            Succeeded = category is not null
        };
    }
}
