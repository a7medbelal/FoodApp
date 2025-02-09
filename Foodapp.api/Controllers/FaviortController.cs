
using FoodApp.Core.Enums;
using FoodApp.Core.Helper;
using FoodApp.Core.interfaces;
using FoodApp.Core.ViewModle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;

namespace FoodApp.Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FaviortController : ControllerBase
    {
        private readonly IFaviortService _faviortService; 
        public FaviortController(IFaviortService faviortService ) {
            _faviortService = faviortService;
        }

        [HttpPost]
        public async Task<IActionResult> AddToFaviort(AddFaviortViewModel viewModel) {
            var recpeadd =await  _faviortService.Addfaviort(viewModel);
             return Ok(recpeadd);
        }

        [HttpGet]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.getFavRecipe })]
        public async Task<IActionResult> GetUserFavs()
        { 
            var favRecipe =await  _faviortService.GetFaviortsForUser();

            var favlsit= favRecipe.data.ToList();   
       
            return Ok(favlsit);
        }

        [HttpGet("Filter")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.GetFavRecieWithPagination})]
        public async Task<IActionResult> GetUserFavsWithPagination([FromQuery] FavParames favParames)
        {
            var favRecipe = await _faviortService.GetFavResipesByPagination(favParames);

            if (!favRecipe.IsSuccess) return BadRequest(favRecipe.Massage); 

            return Ok(favRecipe);
        }



    }
}
