
using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;
using FoodApp.Core.Helper;

namespace FoodApp.Core.interfaces.RecipeService
{
    public interface IRecpieService  
    {

        ResponsiveView<bool> createRecipes(RecpieCreateViewModel recpieCreate);
        ResponsiveView<bool> DeletRecipe(int id); 
        ResponsiveView<RecpieDetailsViewModel> RecpieDetiles(int id);
        Task<ResponsiveView<Pagination<RecipeViewModel>>> GetAll(RecipeParameter recipe );
        ResponsiveView<IEnumerable<RecpieDetailsViewModel>> getAllAdmin(); 


        ResponsiveView<Recipe> update(UpdateRecipeViewMode updateRecipe ); 







    }
}
