namespace FoodApp.Core.ViewModle.RecpieViewModle
{
    public class RecpieDetailsViewModel
    {
         public string Title { get; set; }

        public string Description { get; set; } 
          
        public decimal price { get; set; }

        public string CategoryName { get; set; }
         
        public List<string> tagname {  get; set; }     


    }
}