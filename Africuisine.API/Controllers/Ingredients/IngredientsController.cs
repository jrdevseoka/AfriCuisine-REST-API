using Africuisine.API.Config;
using Africuisine.Application;
using Africuisine.Application.DTO;
using Africuisine.Application.Interfaces.Error;
using Africuisine.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Africuisine.API.Controllers.Ingredients
{
    public class IngredientsController : APIBaseController<IngredientDM>
    {
        private readonly IIngredientService IngredientService;

        public IngredientsController(
            IErrorService<IngredientDM> error,
            IIngredientService ingredientService
        )
            : base(error)
        {
            IngredientService = ingredientService;
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> Add(CreateIngredientsCommand command)
        {
            try
            {
                var response = await IngredientService.Create(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToPostResponse(exception);
                return BadRequest(error);
            }
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] int page, [FromQuery] int size)
        {
            try
            {
                var response = await IngredientService.GetIngredients(page, size);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToItemsResponse(exception);
                return NotFound(error);
            }
        }

        [HttpGet(":{id}"), Authorize]
        public async Task<IActionResult> GetById([FromQuery] string id)
        {
            try
            {
                var response = await IngredientService.GetIngredientById(id);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToItemResponse(exception);
                return NotFound(error);
            }
        }
        [HttpDelete, Authorize]
        public async Task<IActionResult> Delete(IngredientDTO command)
        {
            try
            {
                var response = await IngredientService.Delete(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToPostResponse(exception);
                return BadRequest(error);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Update(IngredientDTO command)
        {
            try
            {
                var response = await IngredientService.Update(command);
                return Ok(response);
            }
            catch (Exception exception)
            {
                var error = Error.MapErrorToPostResponse(exception);
                return BadRequest(error);
            }
        }
    }
}