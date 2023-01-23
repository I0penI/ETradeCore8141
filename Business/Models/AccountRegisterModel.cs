#nullable disable 
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Business.Models
{
    public class AccountRegisterModel
    {
        [Required(ErrorMessage ="{0} is required")]
        [MaxLength(20, ErrorMessage = "{0} must be max {1} characters" )]
        [MinLength(3, ErrorMessage = "{0} must be min {1} characters" )]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(20, ErrorMessage = "{0} must be max {1} characters")]
        [MinLength(8, ErrorMessage = "{0} must be min {1} characters")]
        [Compare("ConfirmPassword" , ErrorMessage = "Passwords do not match!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} is required")]
        [MaxLength(20, ErrorMessage = "{0} must be max {1} characters")]
        [MinLength(8, ErrorMessage = "{0} must be min {1} characters")]
        [DisplayName("Confirm Password ")]
        public string ConfirmPassword { get; set; }
        
    }
}
