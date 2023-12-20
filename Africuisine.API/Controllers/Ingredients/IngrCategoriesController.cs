using Africuisine.API.Config;
using Africuisine.Application;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Application.Interfaces.Ingredients;
using Africuisine.Application.Res;
using Africuisine.Domain.Ingredients;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Ingredients
{
    public class IngrCategoriesController : APIBaseController<IngredientCategoryDM>
    {
        private readonly IIngrCategoryService CategoryService;

        public IngrCategoriesController(
            IErrorService<IngredientCategoryDM> error,
            IIngrCategoryService categoryService
        )
            : base(error)
        {
            CategoryService = categoryService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Add(CreateCategoryCommand command)
        {
            try
            {
                var response = await CategoryService.Create(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToPostResponse(exception);
                return BadRequest(error);
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery]int size)
        {
            try
            {
                var response = await CategoryService.GetIngredientCategories(page, size);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToItemsResponse(exception);
                return NotFound(error);
            }
        }
        [HttpGet("{id}"), Authorize]
        public async Task<IActionResult> GetById([FromQuery]string id)
        {
            try
            {
                var response = await CategoryService.GetIngredientCategoryById(id);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToItemResponse(exception);
                return NotFound(error);
            }
        }
    }
}
