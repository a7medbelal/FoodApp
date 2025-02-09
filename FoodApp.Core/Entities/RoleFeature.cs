using FoodApp.Core.Enums;
using FoodApp.Core.Entities;


namespace FoodApp.Core.Entities
{
    public class RoleFeature: BaseEntity 
    {
        public Role role { get; set;  } 

        public features feature { get; set;  }  
    }
}
