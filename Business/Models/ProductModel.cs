#nullable disable

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AppCore.Records.Bases;

namespace Business.Models
{
    public class ProductModel : RecordBase
    {
        #region Entity'den kopyalanan özellikler
        [Required]
        [StringLength(200)]
        [DisplayName("Product Name")]
        public string Name { get; set; }

        [StringLength(500)]
		[DisplayName("Description")]
		public string Description { get; set; }
        [DisplayName("Unit Price")]
        public double UnitPrice { get; set; }

		[DisplayName("Stock Amount")]
		public int StockAmount { get; set; }
        [DisplayName("Expiration Date")]
        public DateTime? ExpirationDate { get; set; }

		
		public int CategoryId { get; set; }
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
