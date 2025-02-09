using FoodApp.Core.ViewModle.RecpieViewModle;

namespace FoodApp.Core.ViewModle.categoryViewModel
{

    public class CategoryCreateViewModel
      {
            public string Name { get; set; }
         
      }
    public class CategoryViewModel
      {

        public string Name { get; set; }
      }

    public class CategoryWithRecipesViewModel
    {
       
        public string Name { get; set; }

        public List<RecpieDetailsViewModel> Recipes { get; set; } = new List<RecpieDetailsViewModel>(); 
    }

    public class UpdateCategoryViewModel
      {
    
        public string Name { get; set; }
     
      } 


}
