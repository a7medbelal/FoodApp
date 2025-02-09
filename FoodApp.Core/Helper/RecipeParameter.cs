namespace FoodApp.Core.Helper
{
    public class RecipeParameter : QueryStringParamater 
    {
        public int ? CategoryID { get; set; } 

        public int ?  TagID { get; set; }

        public string ?  name { get; set; }

        public decimal ?  price { get; set; }  

    }

    public class FavParames : QueryStringParamater
    {
        public string? CategoryName { get; set; }
        
        public string? name { get; set; }


        public decimal? price { get; set; }

    }




}
