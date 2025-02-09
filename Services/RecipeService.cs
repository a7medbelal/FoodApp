
using FoodApp.Core.Helper;
using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.RecpieViewModle;
using FoodApp.Core.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using FoodApp.Core.interfaces;
using FoodApp.Core.Enums;

using FoodApp.Core.FoodApp.Service;
using FoodApp.Core.interfaces.RecipeService;
using FoodApp.Core;

namespace FoodApp.Services
{
    public class RecipeService : IRecpieService
    {
        IRepository<Recipe> repository;

        public RecipeService(IRepository<Recipe> repository)
        {
            this.repository = repository;
        }

        public ResponsiveView<bool> createRecipes(RecpieCreateViewModel recpieCreate)
        {
            if (recpieCreate == null) return new FailerResView<bool>(Errorcode.emptyData, "no data have been fatched");

            var recpieExist = repository.Any(c => c.Item.EnName == recpieCreate.Name);

            if (recpieExist) return new FailerResView<bool>(Errorcode.recipeExist, " this recpie is already exist in the system");

            var creatRec = recpieCreate.MapTo<Recipe>();

            repository.Add(creatRec);
            repository.SaveChanges();

            return new SuccessResView<bool>(true, "recipe created sucessfuly ");
        }

        public ResponsiveView<bool> DeletRecipe(int id)
        {
            var entityExist = repository.Get(c => c.ID == id && !c.Deleted).FirstOrDefault();

            if (entityExist is null) return new FailerResView<bool>(Errorcode.alreadyDeleted, "this recipe not found ar already deleted ");

            repository.Delete(entityExist);

            repository.SaveChanges();

            return new SuccessResView<bool>(true, "deleted sucssfuly");
        }

        public async Task<ResponsiveView<Pagination<RecipeViewModel>>> GetAll(RecipeParameter recipeParameter)
        {
            var recipes = repository.GetAll();
            if (!recipes.Any()) return new FailerResView<Pagination<RecipeViewModel>>(Errorcode.unfoundData, "there is no recipes to display ");

            if (!string.IsNullOrEmpty(recipeParameter.name))
                recipes = recipes.Where(c => c.Item.EnName.Contains(recipeParameter.name));

            if (recipeParameter.CategoryID > 0)
                recipes = recipes.Where(r => r.CategoryId == recipeParameter.CategoryID);

            if (recipeParameter.TagID > 0)
                recipes = recipes.Where(r => r.Tags.Any(c => c.TagId == recipeParameter.TagID));

            if (recipeParameter.price > 0)
                recipes = recipes.Where(r => r.price == recipeParameter.price);

            var recipesview = recipes.ProjectTo<RecipeViewModel>();

            var PaginatResult = await Pagination<RecipeViewModel>.ToPagedList(recipesview, recipeParameter.PageNumber, recipeParameter.PageSize);

            return new SuccessResView<Pagination<RecipeViewModel>>(PaginatResult);

        }

        public ResponsiveView<IEnumerable<RecpieDetailsViewModel>> getAllAdmin()
        {
            var recipes = repository.GetAll();
            if (!recipes.Any()) return new FailerResView<IEnumerable<RecpieDetailsViewModel>>(Errorcode.unfoundData, "there is no recipe to display ");


            var recipesview = recipes.ProjectTo<RecpieDetailsViewModel>();

            return new SuccessResView<IEnumerable<RecpieDetailsViewModel>>(recipesview);
        }

        public ResponsiveView<RecpieDetailsViewModel> RecpieDetiles(int id)
        {
            var exists = repository.Get(c => c.ID == id).FirstOrDefault();

            if (exists is null) return new FailerResView<RecpieDetailsViewModel>(Errorcode.unfoundData, " thereis no recipr by this id");

            var quary = exists.MapTo<RecpieDetailsViewModel>();

            return new SuccessResView<RecpieDetailsViewModel>(quary, "this the data about this recpie");

        }

        public ResponsiveView<Recipe> update(UpdateRecipeViewMode updateRecipe)
        {
            if (updateRecipe is null)
                return new FailerResView<Recipe>(Errorcode.emptyData, "faild to match data");



            var recipe = updateRecipe.MapTo<Recipe>();
            if (recipe is null) return new FailerResView<Recipe>(Errorcode.unfoundData, "not fouund ");

            repository.SaveInclude(recipe, nameof(recipe.Description), nameof(recipe.Item.EnName));

            return new SuccessResView<Recipe>(recipe, "updated successfuly");
        }
    }
}
