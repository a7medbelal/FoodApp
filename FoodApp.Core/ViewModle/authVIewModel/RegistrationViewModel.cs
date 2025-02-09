using System.ComponentModel.DataAnnotations;

namespace FoodApp.Core.ViewModle.authVIewModel
{
    public class RegistrationViewModel
    {
        [Required, StringLength(50)]
        public string Fname { get; set; }

        [Required, StringLength(50)]
        public string Lname { get; set; }

        [StringLength(20)]
        public string Username { get; set; }

        [Required, StringLength(50)]
        public string Email { get; set; }

        [Required, StringLength(100)]
        public string Password { get; set; }    
    }
}
