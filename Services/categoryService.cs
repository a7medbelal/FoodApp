using FoodApp.Core;
using FoodApp.Core.Entities;
using FoodApp.Core.Enums;

using FoodApp.Core.interfaces;
using FoodApp.Core.interfaces.RecipeService;
using FoodApp.Core.ViewModle;
using FoodApp.Core.ViewModle.categoryViewModel;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace FoodApp.Services
{
    public class categoryService : IcategoryService
    {
        private readonly IRepository<Category> _repository;
        private readonly IRecpieService _recpieService;

        public categoryService(IRepository<Category> repository, IRecpieService recpieService)
        {
            _repository = repository;
            _recpieService = recpieService;
        }

        public ResponsiveView<bool> CreateCategory(CategoryCreateViewModel categoryCreate)
        {
            if (categoryCreate == null)
                return new FailerResView<bool>(Errorcode.emptyData, "No data has been provided.");

            var categoryExists = _repository.Any(c => c.Name == categoryCreate.Name);

            if (categoryExists)
                return new FailerResView<bool>(Errorcode.categoryExist, "This category already exists in the system.");

            var newCategory = categoryCreate.MapTo<Category>();

            _repository.Add(newCategory);
            _repository.SaveChanges();

            return new SuccessResView<bool>(true, "Category created successfully.");
        }

        public ResponsiveView<bool> DeleteCategory(int id)
        {
            var entity = _repository.Get(c => c.ID == id && !c.Deleted).FirstOrDefault();

            if (entity == null)
                return new FailerResView<bool>(Errorcode.unfoundData, "This category was not found or is already deleted.");

            _repository.Delete(entity);
            _repository.SaveChanges();

            return new SuccessResView<bool>(true, "Category deleted successfully.");
        }

        public ResponsiveView<IEnumerable<CategoryViewModel>> GetAll()
        {
            var categories = _repository.GetAll();

            if (!categories.Any())
                return new FailerResView<IEnumerable<CategoryViewModel>>(Errorcode.unfoundData, "There are no categories to display.");

            var categoryViews = categories.ProjectTo<CategoryViewModel>();

            return new SuccessResView<IEnumerable<CategoryViewModel>>(categoryViews);
        }
        public ResponsiveView<CategoryWithRecipesViewModel> GetCategoryWithRecipes(int id)
        {
            var categries = _repository.Get(c => c.ID == id).Include(c => c.Recipes).ThenInclude(c => c.Item).FirstOrDefault();

            if (categries == null) return new FailerResView<CategoryWithRecipesViewModel>(Errorcode.unfoundData, "catageroy not found");

            var catview = categries.MapTo<CategoryWithRecipesViewModel>();

            return new SuccessResView<CategoryWithRecipesViewModel>(catview, "Category with recipes retrieved successfully.");
        }



    }
}
