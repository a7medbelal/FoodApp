using System.ComponentModel.DataAnnotations;

namespace FoodApp.Core.ViewModle.RecpieViewModle
{
    public class RecpieCreateViewModel
    {
        [Required]
        public string Name { get; set; }    


        [Required , StringLength(128)]
        public string Description { get; set; }
        [Required]
        public int CategoryId { get; set; }

        public  decimal price { get; set; }
        
        public DateTime? CreatedAt { get; set; } = DateTime.UtcNow;   

        [Required]
        public List<int> TagId { get; set; }

    }
}
