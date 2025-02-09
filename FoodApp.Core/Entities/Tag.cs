using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodApp.Core.Entities
{
    public class Tag:BaseEntity
    {

        public string Name { get; set; }
        public ICollection<RecipeTag> Tags { get; set; } = new List<RecipeTag>();
    }
}
