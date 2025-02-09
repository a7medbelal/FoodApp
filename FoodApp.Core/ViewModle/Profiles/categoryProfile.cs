using AutoMapper;
using FoodApp.Core.ViewModle.categoryViewModel;
using FoodApp.Core.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;

namespace FoodApp.Core.ViewModle.Profiles
{
    public class categoryProfile : Profile
    {
        public categoryProfile() {


            CreateMap<CategoryCreateViewModel, Category>();
            CreateMap<Category, CategoryViewModel>();

            CreateMap<Category, CategoryWithRecipesViewModel>();
           
            CreateMap<Recipe, RecipeViewModel>();
            CreateMap<UpdateCategoryViewModel, Category>();


        }
    }
}
