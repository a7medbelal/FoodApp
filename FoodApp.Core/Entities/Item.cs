using FoodApp.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FoodApp.Core.Entities
{
    public class Item 
    {
        [Key]
        public int RecipeID { get; set; }
        
        public string EnName { get; set; }

        public string ARName { get; set; }

        public Recipe Recipe { get; set; }
    }
}
