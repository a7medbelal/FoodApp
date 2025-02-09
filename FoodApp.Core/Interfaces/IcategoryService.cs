using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.categoryViewModel;
using FoodApp.Core.Entities;

namespace FoodApp.Core.interfaces
{
    public interface IcategoryService
    {
        ResponsiveView<bool> CreateCategory(CategoryCreateViewModel categoryCreate);
        ResponsiveView<bool> DeleteCategory(int id);
        ResponsiveView<IEnumerable<CategoryViewModel>> GetAll();

        ResponsiveView<CategoryWithRecipesViewModel> GetCategoryWithRecipes(int id);
       // ResponsiveView<Category> UpdateCategory(UpdateCategoryViewModel updateCategory);
    }
}
