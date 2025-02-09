using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class RecipeTag:BaseEntity
    {
        public int RecipeId { get; set; }
        public Recipe Recipes { get; set; }
        public int TagId { get; set; }
        public Tag Tags { get; set; }
    }
}
