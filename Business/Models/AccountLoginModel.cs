#nullable disable

using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Business.Models
{
	public class AccountLoginModel
	{
		[Required(ErrorMessage = "{0} is required")]
		[MaxLength(20, ErrorMessage = "{0} must be max {1} characters")]
		[MinLength(3, ErrorMessage = "{0} must be min {1} characters")]
		[DisplayName("User Name")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "{0} is required")]
		[MaxLength(20, ErrorMessage = "{0} must be max {1} characters")]
		[MinLength(3, ErrorMessage = "{0} must be min {1} characters")]
		
		public string Password { get; set; }
	}
}
