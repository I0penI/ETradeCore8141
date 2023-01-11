#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class ProductModel : RecordBase
    {
        #region Entity'den kopyalanan özellikler
        [Required(ErrorMessage = "{0} is required!")]
        // [StringLength(200, ErrorMessage = "{0} must be max {1} characters!")]
        [MinLength(2, ErrorMessage = "{0} must be min {1} characters!")] // Product Name must be min 2 characters!
        [MaxLength(200, ErrorMessage = "{0} must be max {1} characters!")]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [StringLength(500, ErrorMessage = "{0} must be max {1} characters!")]       
        [DisplayName("Description")]
		public string Description { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////


        [DisplayName("Unit Price")]
        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, double.MaxValue, ErrorMessage = "{0} must be 0 or bigger number!")]
        public double? UnitPrice { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        
        [DisplayName("Stock Amount")]
        [Required(ErrorMessage = "{0} is required!")]
        [Range(0, 1000000, ErrorMessage = "{0} must be min ({1}-{2})!")]
		public int? StockAmount { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
       
        
        
        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        [DisplayName("Category")]
        [Required(ErrorMessage = "{0} required!")]
        public int? CategoryId { get; set; }
		#endregion

		#region Entitiy Dışındaki Özelleştirmeler

		[DisplayName("Unit Price")]
		public string UnitPriceDisplay { get; set; }

		[DisplayName("Expiration Date")]
		public string ExpirationDateDisplay { get; set; }

		[DisplayName("Category")]
		public string CategoryNameDisplay { get; set; }
        #endregion
    }
}
