#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class CategoryModel : RecordBase
    {
		#region Entityden kopyalanan özellikler
		[Required(ErrorMessage = "{0} is required!")]
		[StringLength(100, ErrorMessage = "{0} must be max {1} characters!")]
		public string Name { get; set; }

        public string Description { get; set; }
		#endregion

		#region Sayfada ihtiyacımız olan özellikler
		[DisplayName("Pruoduct Count")]
		public int? ProductCountDisplay { get; set; }
		#endregion
	}
}
