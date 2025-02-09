using FoodApp.Core.Entities;

namespace FoodApp.Core.Entities
{
    public class Favioret : BaseEntity
    {
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        public int RecipeID { get; set; }

        public Recipe Recipe { get; set; }  

    }
}
