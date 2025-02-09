
using FoodApp.Core.Enums;
using FoodApp.Core.Helper;
using FoodApp.Core.interfaces;
using FoodApp.Core.ViewModle.authVIewModel;
using FoodApp.Core.ViewModle.categoryViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FoodApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IcategoryService icategoryService;

        public CategoryController(IcategoryService icategoryService)
        {
            this.icategoryService = icategoryService;
        }

        [HttpGet("GetAllCategoris")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.GetAllCategory })]
        public IActionResult GetAll()
        {
            if (!ModelState.IsValid) return NotFound();

            var categories = icategoryService.GetAll();
            if (categories == null)return BadRequest("not catagories to display"); 
            return Ok(categories);

        }

        [HttpGet("detiales")]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.GetCategoryByID })]
        public IActionResult Get(int id) {
            if (!ModelState.IsValid) return BadRequest();

            var catagory = icategoryService.GetCategoryWithRecipes(id);

            if (catagory == null) return NotFound();

            return Ok(catagory);

        }


        [HttpPost]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.CreateCategory })]
        public IActionResult CreateCategory(CategoryCreateViewModel viewmodel)
        {
            var category = icategoryService.CreateCategory(viewmodel);
            if (category == null) return BadRequest("empty data");  

            return Ok(category);    
        }

        [HttpDelete]
        [Authorize]
        [TypeFilter(typeof(CoustemAurhorizeFilter), Arguments = new object[] { features.DeleteCategory})]
        public IActionResult DeleteCategory(int id) { 
         var Deleted = icategoryService.DeleteCategory(id); 

            return Ok(); 
        }
        
    }
}