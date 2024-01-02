using Africuisine.Application.Commands.Recipe;
using Africuisine.Application.DTO.Recipe;
using Africuisine.Application.Exceptions;
using Africuisine.Application.Interfaces.Log;
using Africuisine.Application.Interfaces.Recipe;
using Africuisine.Application.Requests.Picture;
using Africuisine.Application.Res;
using Africuisine.Domain.Models.Pictures;
using Africuisine.Domain.Models.Recipes;
using Africuisine.Infrastructure.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Africuisine.Infrastructure.Services.Recipe
{
    public class RecipeService : IRecipeService
    {
        private readonly RecipeDBContext WorkflowContext;
        private readonly IMapper Mapper;
        private readonly INLogger Logger;

        public RecipeService(
            IMapper mapper,
            RecipeDBContext workflowContext,
            INLogger logger
        )
        {
            Mapper = mapper;
            WorkflowContext = workflowContext;
            Logger = logger;
        }

        public async Task<PostResponse> Create(CreateRecipeCommand command)
        {
            using var transaction = WorkflowContext.Database.BeginTransaction();
            try
            {
                var recipe = Mapper.Map<RecipeDM>(command);
                WorkflowContext.Recipes.Add(recipe);
                var recipeInstructions = await AddInstructions(command.Instructions, recipe);
                await AddRecipePictures(command.Pictures, recipe);
                int rows = await WorkflowContext.SaveChangesAsync();
                return new PostResponse
                {
                    Succeeded = rows > 0,
                    Message = $"Sucessfully Added Recipe - {command.Name}"
                };
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                await transaction.DisposeAsync();
                string message =
                    $"An unexpected error occured while adding a recipe. Errror: {ex.Message}";
                Logger.Error(message, ex);
                throw new BadRequestException(HttpStatusCode.BadRequest, message);
            }
        }

        public async Task<PostResponse> Delete(RecipeDTO _recipe)
        {
            var recipe = Mapper.Map<RecipeDM>(_recipe);
            WorkflowContext.Recipes.Remove(recipe);
            return new PostResponse
            {
                Succeeded = await WorkflowContext.SaveChangesAsync() > 0,
                Message = $"Recipe - {_recipe.Name} was deleted from the database."
            };
        }

        public async Task<QueryItemsResponse<RecipeDTO>> GetRecipeByCategory(string id)
        {
            var recipes = await WorkflowContext.Recipes
                .AsNoTracking()
                .ProjectTo<RecipeDTO>(Mapper.ConfigurationProvider)
                .Where(r => r.LCategory == id)
                .ToListAsync();
            return new QueryItemsResponse<RecipeDTO>
            {
                Items = recipes,
                Succeeded = recipes.Count > 0
            };
        }

        public async Task<QueryItemResponse<RecipeDTO>> GetRecipeById(string id)
        {
            var recipes = await WorkflowContext.Recipes
                .AsNoTracking()
                .ProjectTo<RecipeDTO>(Mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(r => r.Id == id);
            return new QueryItemResponse<RecipeDTO>
            {
                Item = recipes,
                Succeeded = recipes is not null
            };
        }

        public Task<QueryItemsResponse<RecipeDTO>> GetRecipeByPopularity(string rating)
        {
            throw new NotImplementedException();
        }

        public async Task<QueryItemsResponse<RecipeDTO>> GetRecipes()
        {
            var recipes = await WorkflowContext.Recipes
                .AsNoTracking()
                .ProjectTo<RecipeDTO>(Mapper.ConfigurationProvider)
                .ToListAsync();
            return new QueryItemsResponse<RecipeDTO>
            {
                Items = recipes,
                Succeeded = recipes.Count > 0
            };
        }

        public async Task<PostResponse> Update(RecipeDTO _recipe)
        {
            var recipe = Mapper.Map<RecipeDM>(_recipe);
            WorkflowContext.Recipes.Add(recipe);
            int rows = await WorkflowContext.SaveChangesAsync();
            return new PostResponse { Succeeded = rows > 0 };
        }

        private async Task<List<InstructionDM>> AddInstructions(
            List<string> descriptions,
            RecipeDM recipe
        )
        {
            List<InstructionDM> instructions = new();
            foreach (string description in descriptions)
            {
                var instruction = new InstructionDM
                {
                    Description = description,
                    LUserUpdate = recipe.LUserUpdate,
                    LRecipe = recipe.Link
                };
                await WorkflowContext.AddAsync(instruction);
                instructions.Add(instruction);
            }
            return instructions;
        }

        private async Task AddRecipePictures(List<PictureSM> pictures, RecipeDM recipe)
        {
            foreach (var picture in pictures)
            {
                var image = Mapper.Map<PictureDM>(picture);
                await WorkflowContext.Pictures.AddAsync(image);
                var recipePicture = new RecipePictureDM
                {
                    LRecipe = recipe.Link,
                    LPicture = image.Link,
                    Link = recipe.LUserUpdate
                };
                await WorkflowContext.RecipePictures.AddAsync(recipePicture);
            }
        }
    }
}
