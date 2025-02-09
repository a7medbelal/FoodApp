
using FoodApp.Core.Enums;
using FoodApp.Core.Helper;
using FoodApp.Core.interfaces.RecipeService;
using FoodApp.Core.ViewModle.authVIewModel;
using FoodApp.Core.ViewModle.RecpieViewModle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FoodApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly IRecpieService _recipeService;
       
   

        public RecipeController(IRecpieService recipeService)
        {
            _recipeService = recipeService;
        }

        [HttpPost("create")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.CreateRecipe })]
        public IActionResult CreateRecipe([FromBody] RecpieCreateViewModel recipeCreate)
        {
            if (!ModelState.IsValid)  return BadRequest(); 
            var result = _recipeService.createRecipes(recipeCreate);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("delete/{id}")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.deleteRecipe })]
        public IActionResult DeleteRecipe(int id)
        {
            var result = _recipeService.DeletRecipe(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("all")]
       
        
        public async Task<IActionResult>  GetAllRecipes([FromQuery]RecipeParameter recipeParameter )
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);  
            var result = await _recipeService.GetAll(recipeParameter);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("all-admin")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.GetAllForAdmin })]
        public IActionResult GetAllRecipesForAdmin()
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _recipeService.getAllAdmin();
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpGet("details/{id}")]

        public IActionResult GetRecipeDetails(int id)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _recipeService.RecpieDetiles(id);
            if (!result.IsSuccess)
                return NotFound(result);

            return Ok(result);
        }

        [HttpPut("update")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.updateRecipe })]
        public IActionResult UpdateRecipe([FromBody] UpdateRecipeViewMode updateRecipe)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var result = _recipeService.update(updateRecipe);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
